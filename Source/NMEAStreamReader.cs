/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// 
    /// </summary>
    public class NMEAStreamReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async IAsyncEnumerable<string> ReadLineAsync(Stream stream)
        {
            List<byte> line = new List<byte>();
            var byteStreamEnumerator = ReadAsync(stream).GetAsyncEnumerator();
            while (true)
            {
                while (await byteStreamEnumerator.MoveNextAsync())
                    if (IsStartByte(byteStreamEnumerator.Current))
                    {
                        line.Add(byteStreamEnumerator.Current);
                        break;
                    }

                while (await byteStreamEnumerator.MoveNextAsync())
                {
                    if (IsStopByte(byteStreamEnumerator.Current)) break;
                    line.Add(byteStreamEnumerator.Current);
                }

                var lineString = Encoding.ASCII.GetString(line.ToArray());
                yield return lineString;

                line = new List<byte>();

            }
        }

        private static async IAsyncEnumerable<byte> ReadAsync(Stream stream)
        {
            Memory<byte> buffer = new Memory<byte>(new byte[1024]);
            byte[] bufferr = new byte[1024];

            while (true)
            {
                //var sizz = await stream.ReadAsync(bufferr, 0, 1024);
                var sizz = await stream.ReadAsync(bufferr.AsMemory(0, 1024));

                foreach (var i in Enumerable.Range(0, sizz))
                {
                    yield return bufferr[i];
                }
            }
        }

        private static bool IsStartByte(byte b)
        {
            return b == 0x2 || b == '$';
        }

        private static bool IsStopByte(byte b)
        {
            return b == 0x3 || b == '\n';
        }
    }
}
