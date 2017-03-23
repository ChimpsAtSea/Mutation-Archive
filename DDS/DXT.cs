﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDS
{
    internal class DXT
    {
        #region DXT 1,3 & 5
        public static byte[] DecodeDXT1(int Height, int Width, byte[] sourceData)
        {
            byte[] destData = new byte[Height * Width * 4];
            Color.ColorRGBA[] color = new Color.ColorRGBA[4];
            int dptr = 0;
            Color.ColorRGBA CColor;
            int CData;
            int ChunksPerHLine = Width / 4;
            bool trans;
            Color.ColorRGBA zeroColor = new Color.ColorRGBA();
            uint c1;
            uint c2;
            if (ChunksPerHLine == 0)
            {
                ChunksPerHLine += 1;
            }
            for (int i = 0; i < (Width * Height); i += 16)
            {
                c1 = (Convert.ToUInt32(sourceData[dptr + 1]) << 8) | (sourceData[dptr]);
                c2 = (Convert.ToUInt32(sourceData[dptr + 3]) << 8) | (sourceData[dptr + 2]);
                if (c1 > c2)
                {
                    trans = false;
                }
                else
                {
                    trans = true;
                }
                color[0] = Color.IntToRGBA(c1);
                color[1] = Color.IntToRGBA(c2);
                if (!(trans))
                {
                    color[2] = Color.GradientColors(color[0], color[1]);
                    color[3] = Color.GradientColors(color[1], color[0]);
                }
                else
                {
                    color[2] = Color.GradientColorsHalf(color[0], color[1]);
                    color[3] = zeroColor;
                }
                CData = (Convert.ToInt32(sourceData[dptr + 4]) << 0) | (Convert.ToInt32(sourceData[dptr + 5]) << 8) | (Convert.ToInt32(sourceData[dptr + 6]) << 16) | (Convert.ToInt32(sourceData[dptr + 7]) << 24);
                int ChunkNum = i / 16;
                long XPos = ChunkNum % ChunksPerHLine;
                long YPos = (ChunkNum - XPos) / ChunksPerHLine;
                long ttmp;
                int sizeh = Height < 4 ? Height : 4;
                int sizew = Width < 4 ? Width : 4;
                for (int x = 0; x <= sizeh - 1; x++)
                {
                    for (int y = 0; y <= sizew - 1; y++)
                    {
                        CColor = color[CData & 3];
                        CData >>= 2;
                        ttmp = ((YPos * 4 + x) * Width + XPos * 4 + y) * 4;
                        destData[ttmp] = CColor.b;
                        destData[ttmp + 1] = CColor.g;
                        destData[ttmp + 2] = CColor.r;
                        destData[ttmp + 3] = CColor.a;
                    }
                }
                dptr += 8;
            }
            return destData;
        }

        public static byte[] DecodeDXT23(int Height, int Width, byte[] sourceData)
        {
            byte[] destData = new byte[Height * Width * 4];
            Color.ColorRGBA[] color = new Color.ColorRGBA[4];
            Color.ColorRGBA CColor;
            int CData;
            int ChunksPerHLine = Width / 4;

            if (ChunksPerHLine == 0)
            {
                ChunksPerHLine += 1;
            }

            for (int i = 0; i <= (Width * Height) - 1; i += 16)
            {
                color[0] = Color.IntToRGBA(Convert.ToUInt32(sourceData[i + 8]) | Convert.ToUInt32(sourceData[i + 9]) << 8);
                color[1] = Color.IntToRGBA(Convert.ToUInt32(sourceData[i + 10]) | Convert.ToUInt32(sourceData[i + 11]) << 8);
                color[2] = Color.GradientColors(color[0], color[1]);
                color[3] = Color.GradientColors(color[1], color[0]);
                CData = (Convert.ToInt32(sourceData[i + 12]) << 0) | (Convert.ToInt32(sourceData[i + 13]) << 8) | (Convert.ToInt32(sourceData[i + 14]) << 16) | (Convert.ToInt32(sourceData[i + 15]) << 24);
                int ChunkNum = i / 16;
                long XPos = ChunkNum % ChunksPerHLine;
                long YPos = (ChunkNum - XPos) / ChunksPerHLine;
                long ttmp;
                int alpha;
                int sizeh = Height < 4 ? Height : 4;
                int sizew = Width < 4 ? Width : 4;

                for (int x = 0; x <= sizeh - 1; x++)
                {
                    alpha = sourceData[i + (2 * x)] | Convert.ToInt32(sourceData[i + (2 * x) + 1]) << 8;
                    for (int y = 0; y <= sizew - 1; y++)
                    {
                        CColor = color[CData & 3];
                        CData >>= 2;
                        CColor.a = (byte)((alpha & 15) * 16);
                        alpha >>= 4;
                        ttmp = ((YPos * 4 + x) * Width + XPos * 4 + y) * 4;
                        destData[ttmp] = CColor.b;
                        destData[ttmp + 1] = CColor.g;
                        destData[ttmp + 2] = CColor.r;
                        destData[ttmp + 3] = CColor.a;
                    }
                }
            }
            return destData;
        }

        public static byte[] DecodeDXT45(int Height, int Width, byte[] sourceData)
        {
            byte[] destData = new byte[Height * Width * 4];
            Color.ColorRGBA[] color = new Color.ColorRGBA[4];
            Color.ColorRGBA CColor;
            int CData;
            int ChunksPerHLine = Width / 4;
            if (ChunksPerHLine == 0)
            {
                ChunksPerHLine += 1;
            }
            for (int i = 0; i <= (Width * Height) - 1; i += 16)
            {
                color[0] = Color.IntToRGBA(Convert.ToUInt32(sourceData[i + 8]) | Convert.ToUInt32(sourceData[i + 9]) << 8);
                color[1] = Color.IntToRGBA(Convert.ToUInt32(sourceData[i + 10]) | Convert.ToUInt32(sourceData[i + 11]) << 8);
                color[2] = Color.GradientColors(color[0], color[1]);
                color[3] = Color.GradientColors(color[1], color[0]);
                CData = (Convert.ToInt32(sourceData[i + 12]) << 0) | (Convert.ToInt32(sourceData[i + 13]) << 8) | (Convert.ToInt32(sourceData[i + 14]) << 16) | (Convert.ToInt32(sourceData[i + 15]) << 24);
                byte[] alpha = new byte[8];
                alpha[0] = sourceData[i];
                alpha[1] = sourceData[i + 1];
                if ((alpha[0] > alpha[1]))
                {
                    alpha[2] = (byte)((6 * alpha[0] + 1 * alpha[1] + 3) / 7);
                    alpha[3] = (byte)((5 * alpha[0] + 2 * alpha[1] + 3) / 7);
                    alpha[4] = (byte)((4 * alpha[0] + 3 * alpha[1] + 3) / 7);
                    alpha[5] = (byte)((3 * alpha[0] + 4 * alpha[1] + 3) / 7);
                    alpha[6] = (byte)((2 * alpha[0] + 5 * alpha[1] + 3) / 7);
                    alpha[7] = (byte)((1 * alpha[0] + 6 * alpha[1] + 3) / 7);
                }
                else
                {
                    alpha[2] = (byte)((4 * alpha[0] + 1 * alpha[1] + 2) / 5);
                    alpha[3] = (byte)((3 * alpha[0] + 2 * alpha[1] + 2) / 5);
                    alpha[4] = (byte)((2 * alpha[0] + 3 * alpha[1] + 2) / 5);
                    alpha[5] = (byte)((1 * alpha[0] + 4 * alpha[1] + 2) / 5);
                    alpha[6] = 0;
                    alpha[7] = 255;
                }
                long tmpdword;
                int tmpword;
                long alphaDat;
                tmpword = sourceData[i + 2] | (Convert.ToInt32(sourceData[i + 3]) << 8);
                tmpdword = sourceData[i + 4] | (Convert.ToInt32(sourceData[i + 5]) << 8) | (sourceData[i + 6] << 16) | (Convert.ToInt32(sourceData[i + 7]) << 24);
                alphaDat = tmpword | (tmpdword << 16);
                int ChunkNum = i / 16;
                long XPos = ChunkNum % ChunksPerHLine;
                long YPos = (ChunkNum - XPos) / ChunksPerHLine;
                long ttmp;
                int sizeh = Height < 4 ? Height : 4;
                int sizew = Width < 4 ? Width : 4;
                for (int x = 0; x <= sizeh - 1; x++)
                {
                    for (int y = 0; y <= sizew - 1; y++)
                    {
                        CColor = color[CData & 3];
                        CData >>= 2;
                        CColor.a = alpha[alphaDat & 7];
                        alphaDat >>= 3;
                        ttmp = ((YPos * 4 + x) * Width + XPos * 4 + y) * 4;
                        destData[ttmp] = CColor.b;
                        destData[ttmp + 1] = CColor.g;
                        destData[ttmp + 2] = CColor.r;
                        destData[ttmp + 3] = CColor.a;
                    }
                }
            }
            return destData;
        }
        #endregion

        #region 2D_Texture
        public static byte[] DecodeDXT1Texture(int Width, int Height, byte[] SourceData)
        {
            DDS.Color.ColorRGBA[] Color = new DDS.Color.ColorRGBA[5];

            DDS.Color.ColorRGBA CColor;
            DDS.Color.ColorRGBA zeroColor;

            int CData;
            int c1;
            int c2;
            int dptr = 0;

            bool trans;
            byte[] DestData = new byte[(Width * Height) * 4];

            int ChunksPerHLine = Width / 4;
            if (ChunksPerHLine == 0) ChunksPerHLine = 1;

            for (int i = 0; i < (Width * Height); i += 16)
            {
                c1 = (SourceData[dptr + 1] << 8) | (SourceData[dptr]);
                c2 = (SourceData[dptr + 3] << 8) | (SourceData[dptr + 2]);

                trans = (!(c1 > c2));

                Color[0] = DDS.Color.ShortToColor(c1);
                Color[1] = DDS.Color.ShortToColor(c2);

                if (!trans)
                {
                    Color[2] = DDS.Color.GradientColors(Color[0], Color[1]);
                    Color[3] = DDS.Color.GradientColors(Color[1], Color[0]);
                }
                else
                {
                    zeroColor = Color[0];
                    Color[2] = DDS.Color.GradientColorsHalf(Color[0], Color[1]);
                    Color[3] = zeroColor;
                }

                CData = (SourceData[dptr + 4] << 0) | (SourceData[dptr + 5] << 8) |
                    (SourceData[dptr + 6] << 16) | (SourceData[dptr + 7] << 24);

                int ChunkNum = i / 16;
                long XPos = ChunkNum % ChunksPerHLine;
                long YPos = (ChunkNum - XPos) / ChunksPerHLine;

                long tmp1, tmp2;

                int sizeh = Height < 4 ? Height : 4;
                int sizew = Width < 4 ? Width : 4;

                int x, y;
                for (x = 0; x < sizeh; x++)
                {
                    for (y = 0; y < sizew; y++)
                    {
                        CColor = Color[CData & 3];
                        CData >>= 2;
                        tmp1 = ((YPos * 4 + x) * Width + XPos * 4 + y) * 4;
                        tmp2 = DDS.Color.ColorToInt(CColor);
                        DestData[tmp1] = CColor.b;
                        DestData[tmp1 + 1] = CColor.g;
                        DestData[tmp1 + 2] = CColor.r;
                        DestData[tmp1 + 3] = CColor.a;
                    }
                }
                dptr += 8;
            }
            return DestData;
        }

        public static byte[] DecodeDXT23Texture(int Width, int Height, byte[] SourceData)
        {
            DDS.Color.ColorRGBA[] Color = new DDS.Color.ColorRGBA[5];

            DDS.Color.ColorRGBA CColor;
            DDS.Color.ColorRGBA c1, c2, c3, c4;
            int CData;
            byte[] DestData = new byte[(Width * Height) * 4];

            int ChunksPerHLine = Width / 4;
            if (ChunksPerHLine == 0) ChunksPerHLine = 1;

            for (int i = 0; i < (Width * Height); i += 16)
            {
                c1 = DDS.Color.ShortToColor((SourceData[i + 8]) | (SourceData[i + 9] << 8));
                c2 = DDS.Color.ShortToColor((SourceData[i + 10]) | (SourceData[i + 11] << 8));
                c3 = DDS.Color.GradientColors(Color[0], Color[1]);
                c4 = DDS.Color.GradientColors(Color[1], Color[0]);
                Color[0] = c1;
                Color[1] = c2;
                Color[2] = c3;
                Color[3] = c4;

                CData = (SourceData[i + 12] << 0) | (SourceData[i + 13] << 8) | (SourceData[i + 14] << 16) | (SourceData[i + 15] << 24);

                int ChunkNum = i / 16;
                long XPos = ChunkNum % ChunksPerHLine;
                long YPos = (ChunkNum - XPos) / ChunksPerHLine;

                long ttmp;

                int alpha;
                int sizeh = Height < 4 ? Height : 4;
                int sizew = Width < 4 ? Width : 4;
                int x, y;

                for (x = 0; x < sizeh; x++)
                {
                    alpha = SourceData[i + (2 * x)] | (SourceData[i + (2 * x) + 1]) << 8;
                    for (y = 0; y < sizew; y++)
                    {
                        CColor = Color[CData & 3];
                        CData >>= 2;
                        CColor.a = (byte)((alpha & 15) * 16);
                        alpha >>= 4;
                        ttmp = ((YPos * 4 + x) * Width + XPos * 4 + y) * 4;

                        DestData[ttmp] = CColor.b;
                        DestData[ttmp + 1] = CColor.g;
                        DestData[ttmp + 2] = CColor.r;
                        DestData[ttmp + 3] = CColor.a;
                    }
                }
            }
            return DestData;
        }

        public static byte[] DecodeDXT45Texture(int Width, int Height, byte[] SourceData)
        {
            DDS.Color.ColorRGBA[] Color = new DDS.Color.ColorRGBA[4];
            DDS.Color.ColorRGBA CColor;

            int CData;
            byte[] DestData = new byte[(Width * Height) * 4];

            int ChunksPerHLine = Width / 4;
            if (ChunksPerHLine == 0) ChunksPerHLine = 1;

            for (int i = 0; i < (Width * Height); i += 16)
            {
                Color[0] = DDS.Color.ShortToColor(SourceData[i + 8] | (SourceData[i + 9] << 8));
                Color[1] = DDS.Color.ShortToColor(SourceData[i + 10] | (SourceData[i + 11] << 8));
                Color[2] = DDS.Color.GradientColors(Color[0], Color[1]);
                Color[3] = DDS.Color.GradientColors(Color[1], Color[0]);

                CData = (SourceData[i + 12] << 0) | (SourceData[i + 13] << 8) | (SourceData[i + 14] << 16) | (SourceData[i + 15] << 24);

                byte[] Alpha = new byte[8];
                Alpha[0] = SourceData[i];
                Alpha[1] = SourceData[i + 1];

                //Do the alphas
                if (Alpha[0] > Alpha[1])
                {
                    // 8-alpha block:  derive the other six alphas.
                    // Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.

                    Alpha[2] = (byte)((6 * Alpha[0] + 1 * Alpha[1] + 3) / 7); // bit code 010
                    Alpha[3] = (byte)((5 * Alpha[0] + 2 * Alpha[1] + 3) / 7); // bit code 011
                    Alpha[4] = (byte)((4 * Alpha[0] + 3 * Alpha[1] + 3) / 7); // bit code 100
                    Alpha[5] = (byte)((3 * Alpha[0] + 4 * Alpha[1] + 3) / 7); // bit code 101
                    Alpha[6] = (byte)((2 * Alpha[0] + 5 * Alpha[1] + 3) / 7); // bit code 110
                    Alpha[7] = (byte)((1 * Alpha[0] + 6 * Alpha[1] + 3) / 7); // bit code 111
                }
                else
                {
                    // 6-alpha block.
                    // Bit code 000 = alpha_0, 001 = alpha_1, others are interpolated.
                    Alpha[2] = (byte)((4 * Alpha[0] + 1 * Alpha[1] + 2) / 5); // Bit code 010
                    Alpha[3] = (byte)((3 * Alpha[0] + 2 * Alpha[1] + 2) / 5); // Bit code 011
                    Alpha[4] = (byte)((2 * Alpha[0] + 3 * Alpha[1] + 2) / 5); // Bit code 100
                    Alpha[5] = (byte)((1 * Alpha[0] + 4 * Alpha[1] + 2) / 5); // Bit code 101
                    Alpha[6] = 0;            // Bit code 110
                    Alpha[7] = 255;          // Bit code 111
                }

                // Byte	Alpha
                // 0	Alpha_0
                // 1	Alpha_1 
                // 2	(0)(2) (2 LSBs), (0)(1), (0)(0)
                // 3	(1)(1) (1 LSB), (1)(0), (0)(3), (0)(2) (1 MSB)
                // 4	(1)(3), (1)(2), (1)(1) (2 MSBs)
                // 5	(2)(2) (2 LSBs), (2)(1), (2)(0)
                // 6	(3)(1) (1 LSB), (3)(0), (2)(3), (2)(2) (1 MSB)
                // 7	(3)(3), (3)(2), (3)(1) (2 MSBs)
                // (0

                // Read an int and a short
                int tmpword = SourceData[i + 2] | (SourceData[i + 3] << 8);
                long tmpdword = SourceData[i + 4] | (SourceData[i + 5] << 8) | (SourceData[i + 6] << 16) | (SourceData[i + 7] << 24);

                long alphaDat = tmpword | (tmpdword << 16);

                int ChunkNum = i / 16;
                long XPos = ChunkNum % ChunksPerHLine;
                long YPos = (ChunkNum - XPos) / ChunksPerHLine;
                long ttmp;
                int sizeh = Height < 4 ? Height : 4;
                int sizew = Width < 4 ? Width : 4;
                int x, y;
                for (x = 0; x < sizeh; x++)
                {
                    for (y = 0; y < sizew; y++)
                    {
                        CColor = Color[CData & 3];
                        CData >>= 2;
                        CColor.a = Alpha[alphaDat & 7];
                        alphaDat >>= 3;
                        ttmp = ((YPos * 4 + x) * Width + XPos * 4 + y) * 4;
                        DestData[ttmp] = CColor.b;
                        DestData[ttmp + 1] = CColor.g;
                        DestData[ttmp + 2] = CColor.r;
                        DestData[ttmp + 3] = CColor.a;
                    }
                }
            }
            return DestData;
        }

        public static byte[] DecodeU8V8(float Width, float Height, byte[] SourceData, int Count, float BitsPerPixel)
        {
            float PreviewSize = (Width * Height) / (8 / BitsPerPixel);
            byte[] DecodedData = new byte[(int)PreviewSize];
            //int MipSize = 0;
            //for (int x = 0; x < Count; x++)
            //{
            //    Height /= 2;
            //    Bitmap.Width /= 2;
            //    MipSize += (Height * Bitmap.Width);
            //}
            for (int x = 0; x < (int)PreviewSize; x++)
            {
                DecodedData[x] = SourceData[x];
            }
            return DecodedData;

        }
        #endregion

        #region Cube Map
        public static byte[] DecodeCubeDXT1(int width, int height, byte[] SourceData)
        {
            DDS.Color.ColorRGBA[] Color = new DDS.Color.ColorRGBA[5];

            DDS.Color.ColorRGBA CColor;
            DDS.Color.ColorRGBA zeroColor;

            int CData;
            int c1;
            int c2;
            int dptr = 0;

            bool trans;
            byte[] DestData = new byte[(width * height) * 4];

            int ChunksPerHLine = width / 4;
            if (ChunksPerHLine == 0) ChunksPerHLine = 1;

            for (int i = 0; i < (width * height); i += 16)
            {
                c1 = (SourceData[dptr + 1] << 8) | (SourceData[dptr]);
                c2 = (SourceData[dptr + 3] << 8) | (SourceData[dptr + 2]);

                trans = (!(c1 > c2));

                Color[0] = DDS.Color.ShortToColor(c1);
                Color[1] = DDS.Color.ShortToColor(c2);

                if (!trans)
                {
                    Color[2] = DDS.Color.GradientColors(Color[0], Color[1]);
                    Color[3] = DDS.Color.GradientColors(Color[1], Color[0]);
                }
                else
                {
                    zeroColor = Color[0];
                    Color[2] = DDS.Color.GradientColorsHalf(Color[0], Color[1]);
                    Color[3] = zeroColor;
                }

                CData = (SourceData[dptr + 4] << 0) | (SourceData[dptr + 5] << 8) |
                    (SourceData[dptr + 6] << 16) | (SourceData[dptr + 7] << 24);

                int ChunkNum = i / 16;
                long XPos = ChunkNum % ChunksPerHLine;
                long YPos = (ChunkNum - XPos) / ChunksPerHLine;

                long tmp1, tmp2;

                int sizeh = height < 4 ? height : 4;
                int sizew = width < 4 ? width : 4;

                int x, y;
                for (x = 0; x < sizeh; x++)
                {
                    for (y = 0; y < sizew; y++)
                    {
                        CColor = Color[CData & 3];
                        CData >>= 2;
                        tmp1 = ((YPos * 4 + x) * width + XPos * 4 + y) * 4;
                        tmp2 = DDS.Color.ColorToInt(CColor);
                        DestData[tmp1] = CColor.b;
                        DestData[tmp1 + 1] = CColor.g;
                        DestData[tmp1 + 2] = CColor.r;
                        DestData[tmp1 + 3] = CColor.a;
                    }
                }
                dptr += 8;
            }
            return DestData;
        }
        #endregion

        #region ETC
        public static int CalculateTrueDimensions(int Height, int width, BitmapFormat Format, int Rawsize, int BitsPerPixel)
        {
            switch (Format)
            {
                case BitmapFormat.A8R8G8B8:
                    {
                        Rawsize = (int)(Rawsize * (8F / (float)BitsPerPixel));//Size directly related to Dimensions.
                        int Width = (int)(Rawsize / Height);
                        if (Width % 8 != 0)
                        {
                            float Remainder = (float)Width % 8F;
                            Width = (int)(Width - Remainder);
                        }
                        if ((Width - width) % 8 == 0 && (Width - width) >= 16 || (Width - width) <= -16)
                        {
                            Width = (Width - (Width - width));
                        }
                        return Width;
                    }
                case BitmapFormat.A4R4G4B4:
                    {
                        Rawsize = (int)(Rawsize * (8F / (float)BitsPerPixel));//Size directly related to Dimensions.
                        int Width = (int)(Rawsize / Height);
                        if (Width % 8 != 0)
                        {
                            float Remainder = (float)Width % 8F;
                            Width = (int)(Width - Remainder);
                        }
                        if ((Width - width) % 8 == 0 && (Width - width) >= 16 || (Width - width) <= -16)
                        {
                            Width = (Width - (Width - width));
                        }
                        return Width;
                    }
                case BitmapFormat.DXT3:
                    {
                        if (width % 4 != 0)
                        {
                            width = width - (width % 4);
                            return width;
                        }
                    }
                    break;
                case BitmapFormat.A8:
                    {
                        Rawsize = (int)(Rawsize * (8F / (float)BitsPerPixel));//Size directly related to Dimensions.
                        int Width = (int)(Rawsize / Height);
                        if (Width % 8 != 0)
                        {
                            float Remainder = (float)Width % 8F;
                            Width = (int)(Width - Remainder);
                        }
                        if ((Width - width) % 8 == 0 && (Width - width) == 16 || (Width - width) == -16)
                        {
                            Width = (Width - (Width - width));
                        }
                        return Width;
                    }
                case BitmapFormat.AY8:
                    {
                        if (Height == width) { return width; }
                        Rawsize = (int)(Rawsize * (8F / (float)BitsPerPixel));//Size directly related to Dimensions.
                        int Width = (int)(Rawsize / Height);
                        if (Width % 8 != 0)
                        {
                            float Remainder = (float)Width % 8F;
                            Width = (int)(Width - Remainder);
                        }
                        if ((Width - width) % 8 == 0 && (Width - width) == 16 || (Width - width) == -16)
                        {
                            Width = (Width - (Width - width));
                        }
                        return Width;
                    }
                case BitmapFormat.A8Y8:
                    {
                        if (width % 8 == 0 && Height == width || 2 * (Height) == width || 2 * (width) == Height) { return width; }
                        Rawsize = (int)(Rawsize * (8F / (float)BitsPerPixel));//Size directly related to Dimensions.
                        int Width = (int)(Rawsize / Height);
                        if (Width % 8 != 0)
                        {
                            float Remainder = (float)Width % 8F;
                            Width = (int)(Width - Remainder);
                        }
                        return Width;
                    }
            }
            return width;
        }
        public static int CalculateCubemapPadding(int Width, int Height, int MipCount, float BitsPerPixel, int RawSize)
        {
            int Padding = (Height * Width);
            for (int x = 0; x < MipCount; x++)
            {
                Width /= 2;
                Height /= 2;
                Padding += (Height * Width);
            }
            Padding *= 6;
            Padding = (int)(Padding / (8f / BitsPerPixel));
            Padding = RawSize - Padding;
            return Padding / 6;
        }
        public static byte CalculateUVW(byte Green, byte Red)
        {
            int Blue = ((255 * 255) - ((Green * Green) + (Red * Red))) / 255;
            return (byte)Blue;
        }
        public static byte CalculateU8V8Color(byte Color)
        {
            if (Color == 0)
            {
                Color = 127;
                return Color;
            }
            else
            {
                Color += 127;
                return (byte)Color;
            }
        }

        public static byte[] DecodeCubemap(int Height, byte[] SourceData, int BitsPerPixel, int Count, int Padding)
        {
            double FaceBlockSize;
            double FaceSize = FaceBlockSize = Height * Height;
            for (int x = 0; x < Count; x++)
            {
                Height /= 2;
                FaceBlockSize += Height * Height;
            }
            FaceSize = FaceSize / ((double)8 / (double)BitsPerPixel);
            FaceBlockSize = FaceBlockSize / ((double)8 / (double)BitsPerPixel);
            List<byte[]> Faces = new List<byte[]>();
            for (int x = 0; x < 6; x++)
            {
                byte[] Face = new byte[(int)FaceSize];
                for (int i = 0; i < FaceSize; i++)
                {
                    int Index = i + (x * (int)FaceBlockSize) + (x * Padding);
                    Face[i] = SourceData[Index];
                }
                Faces.Add(Face);
            }
            byte[] CubeData = new byte[(int)FaceSize * 6];
            byte[][] FaceData = Faces.ToArray();
            for (int x = 0; x < 6; x++)
            {
                for (int i = 0; i < FaceSize; i++)
                {
                    int Index = (int)(x * FaceSize) + i;
                    CubeData[Index] = FaceData[x][i];
                }
            }
            return CubeData;
        }
        #endregion
    }
}
