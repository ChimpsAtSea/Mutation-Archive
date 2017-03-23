﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IO
{
    public class EndianWriter : BinaryWriter
    {
        #region Fields

        Endianness endian;

        #endregion

        #region Constructor

        public EndianWriter(Endianness Endian, Stream Output)
            : base(Output)
        {
            this.endian = Endian;
        }

        public EndianWriter(Endianness Endian, Stream output, Encoding encoding)
            : base(output, encoding)
        {
            this.endian = Endian;
        }

        #endregion

        #region Methods

        public override void Write(double value)
        {
            Write(endian, value);
        }

        public void Write(Endianness Endian, double value)
        {
            // Get Bytes
            byte[] Data = BitConverter.GetBytes(value);

            // Endian Swap
            if (Endian == Endianness.Big)
                Array.Reverse(Data);

            // Write
            base.Write(Data);
        }

        public override void Write(float value)
        {
            Write(endian, value);
        }

        public void Write(Endianness Endian, float value)
        {
            // Get Bytes
            byte[] Data = BitConverter.GetBytes(value);

            // Endian Swap
            if (Endian == Endianness.Big)
                Array.Reverse(Data);

            // Write
            base.Write(Data);
        }

        public override void Write(int value)
        {
            Write(endian, value);
        }

        public void Write(Endianness Endian, int value)
        {
            // Get Bytes
            byte[] Data = BitConverter.GetBytes(value);

            // Endian Swap
            if (Endian == Endianness.Big)
                Array.Reverse(Data);

            // Write
            base.Write(Data);
        }

        public override void Write(long value)
        {
            Write(endian, value);
        }

        public void Write(Endianness Endian, long value)
        {
            // Get Bytes
            byte[] Data = BitConverter.GetBytes(value);

            // Endian Swap
            if (Endian == Endianness.Big)
                Array.Reverse(Data);

            // Write
            base.Write(Data);
        }

        public override void Write(short value)
        {
            Write(endian, value);
        }

        public void Write(Endianness Endian, short value)
        {
            // Get Bytes
            byte[] Data = BitConverter.GetBytes(value);

            // Endian Swap
            if (Endian == Endianness.Big)
                Array.Reverse(Data);

            // Write
            base.Write(Data);
        }

        public override void Write(uint value)
        {
            Write(endian, value);
        }

        public void Write(Endianness Endian, uint value)
        {
            // Get Bytes
            byte[] Data = BitConverter.GetBytes(value);

            // Endian Swap
            if (Endian == Endianness.Big)
                Array.Reverse(Data);

            // Write
            base.Write(Data);
        }

        public override void Write(ulong value)
        {
            Write(endian, value);
        }

        public void Write(Endianness Endian, ulong value)
        {
            // Get Bytes
            byte[] Data = BitConverter.GetBytes(value);

            // Endian Swap
            if (Endian == Endianness.Big)
                Array.Reverse(Data);

            // Write
            base.Write(Data);
        }

        public override void Write(ushort value)
        {
            Write(endian, value);
        }

        public void Write(Endianness Endian, ushort value)
        {
            // Get Bytes
            byte[] Data = BitConverter.GetBytes(value);

            // Endian Swap
            if (Endian == Endianness.Big)
                Array.Reverse(Data);

            // Write
            base.Write(Data);
        }

        public void WriteNullTerminatingString(string Value)
        {
            WriteNullTerminatingString(Value, Value.Length + 1);
        }

        public void WriteNullTerminatingString(string Value, int MaxLength)
        {
            // Write String
            base.Write(Value.ToCharArray());

            // Write Padding
            base.Write(new byte[MaxLength - Value.Length]);
        }

        public void WriteUnicodeString(string Value, int MaxLength)
        {
            WriteUnicodeString(endian, Value, MaxLength);
        }

        public void WriteUnicodeString(Endianness Endian, string Value, int MaxLength)
        {
            // Write Unicode String
            for (int i = 0; i < Value.Length; i++)
            {
                // Endian Swap
                if (Endian == Endianness.Big)
                {
                    base.Write((byte)0);
                    base.Write(Value[i]);
                }
                else
                {
                    base.Write(Value[i]);
                    base.Write((byte)0);
                }
            }

            // Write Padding
            base.Write(new byte[(MaxLength - Value.Length) * 2]);
        }

        public void AlignToBoundary(int alignment)
        {
            AlignToBoundary(alignment, 0x00);
        }

        public void AlignToBoundary(int alignment, byte value)
        {
            // Compute the padding size.
            int padding = alignment - ((int)base.BaseStream.Position % alignment);

            // Check if it is necessary to write any padding.
            if (padding != alignment)
            {
                // Write padding to stream.
                for (int i = 0; i < padding; i++)
                    Write(value);
            }
        }

        #endregion
    }
}
