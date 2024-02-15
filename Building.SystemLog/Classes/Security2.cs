using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace DIPBMS.SystemLog.Classes
{
    public enum EncryptionAlgorithm
    {
        DES,
        Rc2,
        Rijndael,
        TripleDes
    }
    public class AESEncrypt
    {
        // Fields
        private const int BlockSize = 0x10;
        private byte[,] iSbox;
        private byte[] key;
        private int Nb;
        private int Nk;
        private int Nr;
        private byte[,] Rcon;
        private byte[,] Sbox;
        private byte[,] State;
        private byte[,] w;

        // Methods
        public AESEncrypt(KeySize keySize, string Password)
        {
            byte[] keyBytes = this.ConvertStringToByteArray(Password);
            this.Init(keySize, keyBytes);
        }

        public AESEncrypt(KeySize keySize, byte[] keyBytes)
        {
            this.Init(keySize, keyBytes);
        }

        private void AddRoundKey(int round)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.State[i, j] = (byte)(this.State[i, j] ^ this.w[(round * 4) + j, i]);
                }
            }
        }

        private void AES_ShowError(Exception excep, string MyLibtionName)
        {
            string str = "";
            string str2 = "\r\n";
            try
            {
                MessageBox.Show((((str + "An error has ocurred." + str2) + "MyLibtion: clsAES." + MyLibtionName.Trim() + str2) + "Source: " + excep.Source + str2) + "Error Description: " + str2 + excep.Message, "AES Encryption", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch
            {
            }
        }

        private void BuildInvSbox()
        {
            this.iSbox = new byte[,] { { 0x52, 9, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb }, { 0x7c, 0xe3, 0x39, 130, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb }, { 0x54, 0x7b, 0x94, 50, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 11, 0x42, 250, 0xc3, 0x4e }, { 8, 0x2e, 0xa1, 0x66, 40, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25 }, { 0x72, 0xf8, 0xf6, 100, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92 }, { 0x6c, 0x70, 0x48, 80, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 70, 0x57, 0xa7, 0x8d, 0x9d, 0x84 }, { 0x90, 0xd8, 0xab, 0, 140, 0xbc, 0xd3, 10, 0xf7, 0xe4, 0x58, 5, 0xb8, 0xb3, 0x45, 6 }, { 0xd0, 0x2c, 30, 0x8f, 0xca, 0x3f, 15, 2, 0xc1, 0xaf, 0xbd, 3, 1, 0x13, 0x8a, 0x6b }, { 0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 220, 0xea, 0x97, 0xf2, 0xcf, 0xce, 240, 180, 230, 0x73 }, { 150, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 110 }, { 0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 14, 170, 0x18, 190, 0x1b }, { 0xfc, 0x56, 0x3e, 0x4b, 0xc6, 210, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 120, 0xcd, 90, 0xf4 }, { 0x1f, 0xdd, 0xa8, 0x33, 0x88, 7, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f }, { 0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 13, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef }, { 160, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 200, 0xeb, 0xbb, 60, 0x83, 0x53, 0x99, 0x61 }, { 0x17, 0x2b, 4, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 20, 0x63, 0x55, 0x21, 12, 0x7d } };
        }

        private void BuildRcon()
        {
            this.Rcon = new byte[,] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 2, 0, 0, 0 }, { 4, 0, 0, 0 }, { 8, 0, 0, 0 }, { 0x10, 0, 0, 0 }, { 0x20, 0, 0, 0 }, { 0x40, 0, 0, 0 }, { 0x80, 0, 0, 0 }, { 0x1b, 0, 0, 0 }, { 0x36, 0, 0, 0 } };
        }

        private void BuildSbox()
        {
            this.Sbox = new byte[,] { { 0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 1, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76 }, { 0xca, 130, 0xc9, 0x7d, 250, 0x59, 0x47, 240, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0 }, { 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15 }, { 4, 0xc7, 0x23, 0xc3, 0x18, 150, 5, 0x9a, 7, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75 }, { 9, 0x83, 0x2c, 0x1a, 0x1b, 110, 90, 160, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84 }, { 0x53, 0xd1, 0, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 190, 0x39, 0x4a, 0x4c, 0x58, 0xcf }, { 0xd0, 0xef, 170, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 2, 0x7f, 80, 60, 0x9f, 0xa8 }, { 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 210 }, { 0xcd, 12, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 100, 0x5d, 0x19, 0x73 }, { 0x60, 0x81, 0x4f, 220, 0x22, 0x2a, 0x90, 0x88, 70, 0xee, 0xb8, 20, 0xde, 0x5e, 11, 0xdb }, { 0xe0, 50, 0x3a, 10, 0x49, 6, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79 }, { 0xe7, 200, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 8 }, { 0xba, 120, 0x25, 0x2e, 0x1c, 0xa6, 180, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a }, { 0x70, 0x3e, 0xb5, 0x66, 0x48, 3, 0xf6, 14, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e }, { 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 30, 0x87, 0xe9, 0xce, 0x55, 40, 0xdf }, { 140, 0xa1, 0x89, 13, 0xbf, 230, 0x42, 0x68, 0x41, 0x99, 0x2d, 15, 0xb0, 0x54, 0xbb, 0x16 } };
        }

        private byte[] Cipher(byte[] input)
        {
            byte[] buffer = new byte[0x10];
            try
            {
                int num;
                this.State = new byte[4, this.Nb];
                for (num = 0; num < (4 * this.Nb); num++)
                {
                    this.State[num % 4, num / 4] = input[num];
                }
                this.AddRoundKey(0);
                for (int i = 1; i <= (this.Nr - 1); i++)
                {
                    this.SubBytes();
                    this.ShiftRows();
                    this.MixColumns();
                    this.AddRoundKey(i);
                }
                this.SubBytes();
                this.ShiftRows();
                this.AddRoundKey(this.Nr);
                for (num = 0; num < (4 * this.Nb); num++)
                {
                    buffer[num] = this.State[num % 4, num / 4];
                }
            }
            catch (Exception exception)
            {
                this.AES_ShowError(exception, "Cipher");
            }
            return buffer;
        }

        private string ConvertByteArrayToString(byte[] ByteToConvert)
        {
            string str = "";
            for (int i = 0; i < ByteToConvert.Length; i++)
            {
                if (ByteToConvert[i] > 0)
                {
                    str = str + Convert.ToChar(ByteToConvert[i]);
                }
            }
            return str;
        }

        private byte[] ConvertStringToByteArray(string StrToConvert)
        {
            return Encoding.ASCII.GetBytes(StrToConvert);
        }

        public byte[] Decrypt(byte[] input)
        {
            int sourceIndex = 0;
            int length = input.Length;
            byte[] destinationArray = new byte[0x10];
            byte[] buffer2 = new byte[0x10];
            byte[] buffer3 = new byte[input.Length];
            try
            {
                for (sourceIndex = 0; sourceIndex < length; sourceIndex += 0x10)
                {
                    Array.Copy(input, sourceIndex, destinationArray, 0, 0x10);
                    Array.Copy(this.InvCipher(destinationArray), 0, buffer3, sourceIndex, 0x10);
                }
            }
            catch (Exception exception)
            {
                this.AES_ShowError(exception, "Decrypt");
            }
            return buffer3;
        }

        public string Decrypt(string input)
        {
            string str = "";
            int index = 0;
            int length = 0;
            try
            {
                string[] strArray = input.Split(new char[] { ' ' });
                length = strArray.Length;
                byte[] buffer = new byte[length];
                for (index = 0; index < length; index++)
                {
                    buffer[index] = Convert.ToByte(strArray[index]);
                }
                str = this.ConvertByteArrayToString(this.Decrypt(buffer));
            }
            catch (Exception exception)
            {
                this.AES_ShowError(exception, "Decrypt");
            }
            return str;
        }

        public void Dump()
        {
            Console.WriteLine(string.Concat(new object[] { "Nb = ", this.Nb, " Nk = ", this.Nk, " Nr = ", this.Nr }));
            Console.WriteLine("\nThe key is \n" + this.DumpKey());
            Console.WriteLine("\nThe Sbox is \n" + this.DumpTwoByTwo(this.Sbox));
            Console.WriteLine("\nThe w array is \n" + this.DumpTwoByTwo(this.w));
            Console.WriteLine("\nThe State array is \n" + this.DumpTwoByTwo(this.State));
        }

        public string DumpKey()
        {
            string str = "";
            for (int i = 0; i < this.key.Length; i++)
            {
                str = str + this.key[i].ToString("x2") + " ";
            }
            return str;
        }

        public string DumpTwoByTwo(byte[,] a)
        {
            string str = "";
            for (int i = 0; i < a.GetLength(0); i++)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "[", i, "] " });
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    str = str + a[i, j].ToString("x2") + " ";
                }
                str = str + "\n";
            }
            return str;
        }

        public byte[] Encrypt(byte[] input)
        {
            int sourceIndex = 0;
            int length = input.Length;
            byte[] destinationArray = new byte[0];
            byte[] buffer3 = new byte[0x10];
            byte[] buffer4 = new byte[0x10];
            int arraySize = 0;
            try
            {
                arraySize = this.GetArraySize(input.Length);
                destinationArray = new byte[arraySize];
                byte[] buffer2 = new byte[arraySize];
                Array.Copy(input, 0, buffer2, 0, input.Length);
                for (sourceIndex = 0; sourceIndex < length; sourceIndex += 0x10)
                {
                    Array.Copy(buffer2, sourceIndex, buffer3, 0, 0x10);
                    Array.Copy(this.Cipher(buffer3), 0, destinationArray, sourceIndex, 0x10);
                }
            }
            catch (Exception exception)
            {
                this.AES_ShowError(exception, "Encrypt");
            }
            return destinationArray;
        }

        public string Encrypt(string input)
        {
            string str = "";
            int index = 0;
            int length = 0;
            try
            {
                byte[] buffer = this.ConvertStringToByteArray(input);
                byte[] buffer2 = this.Encrypt(buffer);
                length = buffer2.Length;
                for (index = 0; index < length; index++)
                {
                    str = str + buffer2[index].ToString().Trim() + " ";
                }
            }
            catch (Exception exception)
            {
                this.AES_ShowError(exception, "Encrypt");
            }
            return str.Trim();
        }

        private int GetArraySize(int ArrayLen)
        {
            if ((0x10 % ArrayLen) == 0)
            {
                return ArrayLen;
            }
            return (((ArrayLen / 0x10) + 1) * 0x10);
        }

        private static byte gfmultby01(byte b)
        {
            return b;
        }

        private static byte gfmultby02(byte b)
        {
            if (b < 0x80)
            {
                return (byte)(b << 1);
            }
            return (byte)((b << 1) ^ 0x1b);
        }

        private static byte gfmultby03(byte b)
        {
            return (byte)(gfmultby02(b) ^ b);
        }

        private static byte gfmultby09(byte b)
        {
            return (byte)(gfmultby02(gfmultby02(gfmultby02(b))) ^ b);
        }

        private static byte gfmultby0b(byte b)
        {
            return (byte)((gfmultby02(gfmultby02(gfmultby02(b))) ^ gfmultby02(b)) ^ b);
        }

        private static byte gfmultby0d(byte b)
        {
            return (byte)((gfmultby02(gfmultby02(gfmultby02(b))) ^ gfmultby02(gfmultby02(b))) ^ b);
        }

        private static byte gfmultby0e(byte b)
        {
            return (byte)((gfmultby02(gfmultby02(gfmultby02(b))) ^ gfmultby02(gfmultby02(b))) ^ gfmultby02(b));
        }

        private void Init(KeySize keySize, byte[] keyBytes)
        {
            int index = 0;
            int length = 0;
            int num3 = 0;
            byte num4 = 1;
            try
            {
                this.SetNbNkNr(keySize);
                this.key = new byte[this.Nk * 4];
                if (this.key.Length == keyBytes.Length)
                {
                    keyBytes.CopyTo(this.key, 0);
                }
                else
                {
                    length = this.key.Length;
                    num3 = keyBytes.Length;
                    for (index = 0; index < length; index++)
                    {
                        if (index < num3)
                        {
                            this.key[index] = keyBytes[index];
                        }
                        else
                        {
                            this.key[index] = (byte)(num4 + 1);
                        }
                    }
                }
                this.BuildSbox();
                this.BuildInvSbox();
                this.BuildRcon();
                this.KeyExpansion();
            }
            catch (Exception exception)
            {
                this.AES_ShowError(exception, "Init");
            }
        }

        private byte[] InvCipher(byte[] input)
        {
            byte[] buffer = new byte[0x10];
            try
            {
                int num;
                this.State = new byte[4, this.Nb];
                for (num = 0; num < (4 * this.Nb); num++)
                {
                    this.State[num % 4, num / 4] = input[num];
                }
                this.AddRoundKey(this.Nr);
                for (int i = this.Nr - 1; i >= 1; i--)
                {
                    this.InvShiftRows();
                    this.InvSubBytes();
                    this.AddRoundKey(i);
                    this.InvMixColumns();
                }
                this.InvShiftRows();
                this.InvSubBytes();
                this.AddRoundKey(0);
                for (num = 0; num < (4 * this.Nb); num++)
                {
                    buffer[num] = this.State[num % 4, num / 4];
                }
            }
            catch (Exception exception)
            {
                this.AES_ShowError(exception, "InvCipher");
            }
            return buffer;
        }

        private void InvMixColumns()
        {
            int num2;
            byte[,] buffer = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                num2 = 0;
                while (num2 < 4)
                {
                    buffer[i, num2] = this.State[i, num2];
                    num2++;
                }
            }
            for (num2 = 0; num2 < 4; num2++)
            {
                this.State[0, num2] = (byte)(((gfmultby0e(buffer[0, num2]) ^ gfmultby0b(buffer[1, num2])) ^ gfmultby0d(buffer[2, num2])) ^ gfmultby09(buffer[3, num2]));
                this.State[1, num2] = (byte)(((gfmultby09(buffer[0, num2]) ^ gfmultby0e(buffer[1, num2])) ^ gfmultby0b(buffer[2, num2])) ^ gfmultby0d(buffer[3, num2]));
                this.State[2, num2] = (byte)(((gfmultby0d(buffer[0, num2]) ^ gfmultby09(buffer[1, num2])) ^ gfmultby0e(buffer[2, num2])) ^ gfmultby0b(buffer[3, num2]));
                this.State[3, num2] = (byte)(((gfmultby0b(buffer[0, num2]) ^ gfmultby0d(buffer[1, num2])) ^ gfmultby09(buffer[2, num2])) ^ gfmultby0e(buffer[3, num2]));
            }
        }

        private void InvShiftRows()
        {
            int num;
            int num2;
            byte[,] buffer = new byte[4, 4];
            for (num = 0; num < 4; num++)
            {
                num2 = 0;
                while (num2 < 4)
                {
                    buffer[num, num2] = this.State[num, num2];
                    num2++;
                }
            }
            for (num = 1; num < 4; num++)
            {
                for (num2 = 0; num2 < 4; num2++)
                {
                    this.State[num, (num2 + num) % this.Nb] = buffer[num, num2];
                }
            }
        }

        private void InvSubBytes()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.State[i, j] = this.iSbox[this.State[i, j] >> 4, this.State[i, j] & 15];
                }
            }
        }

        private void KeyExpansion()
        {
            int num;
            this.w = new byte[this.Nb * (this.Nr + 1), 4];
            for (num = 0; num < this.Nk; num++)
            {
                this.w[num, 0] = this.key[4 * num];
                this.w[num, 1] = this.key[(4 * num) + 1];
                this.w[num, 2] = this.key[(4 * num) + 2];
                this.w[num, 3] = this.key[(4 * num) + 3];
            }
            byte[] word = new byte[4];
            for (num = this.Nk; num < (this.Nb * (this.Nr + 1)); num++)
            {
                word[0] = this.w[num - 1, 0];
                word[1] = this.w[num - 1, 1];
                word[2] = this.w[num - 1, 2];
                word[3] = this.w[num - 1, 3];
                if ((num % this.Nk) == 0)
                {
                    word = this.SubWord(this.RotWord(word));
                    word[0] = (byte)(word[0] ^ this.Rcon[num / this.Nk, 0]);
                    word[1] = (byte)(word[1] ^ this.Rcon[num / this.Nk, 1]);
                    word[2] = (byte)(word[2] ^ this.Rcon[num / this.Nk, 2]);
                    word[3] = (byte)(word[3] ^ this.Rcon[num / this.Nk, 3]);
                }
                else if ((this.Nk > 6) && ((num % this.Nk) == 4))
                {
                    word = this.SubWord(word);
                }
                this.w[num, 0] = (byte)(this.w[num - this.Nk, 0] ^ word[0]);
                this.w[num, 1] = (byte)(this.w[num - this.Nk, 1] ^ word[1]);
                this.w[num, 2] = (byte)(this.w[num - this.Nk, 2] ^ word[2]);
                this.w[num, 3] = (byte)(this.w[num - this.Nk, 3] ^ word[3]);
            }
        }

        private void MixColumns()
        {
            int num2;
            byte[,] buffer = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                num2 = 0;
                while (num2 < 4)
                {
                    buffer[i, num2] = this.State[i, num2];
                    num2++;
                }
            }
            for (num2 = 0; num2 < 4; num2++)
            {
                this.State[0, num2] = (byte)(((gfmultby02(buffer[0, num2]) ^ gfmultby03(buffer[1, num2])) ^ gfmultby01(buffer[2, num2])) ^ gfmultby01(buffer[3, num2]));
                this.State[1, num2] = (byte)(((gfmultby01(buffer[0, num2]) ^ gfmultby02(buffer[1, num2])) ^ gfmultby03(buffer[2, num2])) ^ gfmultby01(buffer[3, num2]));
                this.State[2, num2] = (byte)(((gfmultby01(buffer[0, num2]) ^ gfmultby01(buffer[1, num2])) ^ gfmultby02(buffer[2, num2])) ^ gfmultby03(buffer[3, num2]));
                this.State[3, num2] = (byte)(((gfmultby03(buffer[0, num2]) ^ gfmultby01(buffer[1, num2])) ^ gfmultby01(buffer[2, num2])) ^ gfmultby02(buffer[3, num2]));
            }
        }

        private byte[] RotWord(byte[] word)
        {
            return new byte[] { word[1], word[2], word[3], word[0] };
        }

        private void SetNbNkNr(KeySize keySize)
        {
            this.Nb = 4;
            if (keySize == KeySize.Bits128)
            {
                this.Nk = 4;
                this.Nr = 10;
            }
            else if (keySize == KeySize.Bits192)
            {
                this.Nk = 6;
                this.Nr = 12;
            }
            else if (keySize == KeySize.Bits256)
            {
                this.Nk = 8;
                this.Nr = 14;
            }
        }

        private void ShiftRows()
        {
            int num;
            int num2;
            byte[,] buffer = new byte[4, 4];
            for (num = 0; num < 4; num++)
            {
                num2 = 0;
                while (num2 < 4)
                {
                    buffer[num, num2] = this.State[num, num2];
                    num2++;
                }
            }
            for (num = 1; num < 4; num++)
            {
                for (num2 = 0; num2 < 4; num2++)
                {
                    this.State[num, num2] = buffer[num, (num2 + num) % this.Nb];
                }
            }
        }

        private void SubBytes()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.State[i, j] = this.Sbox[this.State[i, j] >> 4, this.State[i, j] & 15];
                }
            }
        }

        private byte[] SubWord(byte[] word)
        {
            return new byte[] { this.Sbox[word[0] >> 4, word[0] & 15], this.Sbox[word[1] >> 4, word[1] & 15], this.Sbox[word[2] >> 4, word[2] & 15], this.Sbox[word[3] >> 4, word[3] & 15] };
        }

        // Nested Types
        public enum KeySize
        {
            Bits128,
            Bits192,
            Bits256
        }
    }
    public class Decryptor
    {
        // Fields
        private EncryptionAlgorithm AlgoritmID;
        private byte[] IV;

        // Methods
        public Decryptor(EncryptionAlgorithm algID)
        {
            this.AlgoritmID = algID;
        }

        public Decryptor(EncryptionAlgorithm algID, byte[] iv)
        {
            this.AlgoritmID = algID;
            this.IV = iv;
        }

        public string Decrypt(string MainString, string key)
        {
            DecryptTransformer transformer = new DecryptTransformer(this.AlgoritmID, this.IV);
            transformer.SetSecurityKey(key);
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(MainString.Trim()));
            CryptoStream stream2 = new CryptoStream(stream, transformer.GetCryptoTransform(), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(stream2);
            string str = reader.ReadLine();
            reader.Close();
            stream2.Close();
            stream.Close();
            return str;
        }

        // Properties
        public DecryptTransformer DecryptTransformer
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
            }
        }
    }
    public class DecryptTransformer
    {
        // Fields
        private EncryptionAlgorithm algorithmID;
        private bool bHasIV;
        private byte[] IV;
        private string SecurityKey;

        // Methods
        public DecryptTransformer(EncryptionAlgorithm algID)
        {
            this.SecurityKey = "";
            this.bHasIV = false;
            this.algorithmID = algID;
        }

        public DecryptTransformer(EncryptionAlgorithm algID, byte[] iv)
        {
            this.SecurityKey = "";
            this.bHasIV = false;
            this.algorithmID = algID;
            this.IV = iv;
            this.bHasIV = true;
        }

        public ICryptoTransform GetCryptoTransform()
        {
            bool flag = false;
            if (this.SecurityKey.Length != 0)
            {
                flag = true;
            }
            byte[] bytes = Encoding.ASCII.GetBytes(this.SecurityKey);
            switch (this.algorithmID)
            {
                case EncryptionAlgorithm.DES:
                    {
                        DES des = new DESCryptoServiceProvider();
                        if (flag)
                        {
                            des.Key = bytes;
                        }
                        if (this.bHasIV)
                        {
                            des.IV = this.IV;
                        }
                        return des.CreateDecryptor();
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        RC2 rc = new RC2CryptoServiceProvider();
                        if (flag)
                        {
                            rc.Key = bytes;
                        }
                        if (this.bHasIV)
                        {
                            rc.IV = this.IV;
                        }
                        return rc.CreateDecryptor();
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        Rijndael rijndael = new RijndaelManaged();
                        if (flag)
                        {
                            rijndael.Key = bytes;
                        }
                        if (this.bHasIV)
                        {
                            rijndael.IV = this.IV;
                        }
                        return rijndael.CreateDecryptor();
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        TripleDES edes = new TripleDESCryptoServiceProvider();
                        if (flag)
                        {
                            edes.Key = bytes;
                        }
                        if (this.bHasIV)
                        {
                            edes.IV = this.IV;
                        }
                        return edes.CreateDecryptor();
                    }
            }
            throw new CryptographicException("Algorithm ID '" + this.algorithmID + "' not supported.");
        }

        public void SetSecurityKey(string Key)
        {
            this.SecurityKey = Key;
        }

        // Properties
        public EncryptionAlgorithm EncryptionAlgorithm
        {
            get
            {
                return this.algorithmID;
            }
            set
            {
                this.algorithmID = value;
            }
        }
    }
    public class EncryptEngine
    {
        // Fields
        private EncryptionAlgorithm AlgoritmID;
        private bool bWithKey = false;
        private string keyword = "";
        public byte[] Vector;

        // Methods
        public EncryptEngine(EncryptionAlgorithm AlgID, string Key)
        {
            if (Key.Length == 0)
            {
                this.bWithKey = false;
            }
            else
            {
                this.bWithKey = true;
            }
            this.keyword = Key;
            this.AlgoritmID = AlgID;
        }

        public ICryptoTransform GetCryptTransform()
        {
            byte[] bytes = Encoding.ASCII.GetBytes(this.keyword);
            switch (this.AlgoritmID)
            {
                case EncryptionAlgorithm.DES:
                    {
                        DES des = new DESCryptoServiceProvider();
                        des.Mode = CipherMode.CBC;
                        if (this.bWithKey)
                        {
                            des.Key = bytes;
                        }
                        this.Vector = des.IV;
                        return des.CreateEncryptor();
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        RC2 rc = new RC2CryptoServiceProvider();
                        rc.Mode = CipherMode.CBC;
                        if (this.bWithKey)
                        {
                            rc.Key = bytes;
                        }
                        this.Vector = rc.IV;
                        return rc.CreateEncryptor();
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        Rijndael rijndael = new RijndaelManaged();
                        rijndael.Mode = CipherMode.CBC;
                        if (this.bWithKey)
                        {
                            rijndael.Key = bytes;
                        }
                        this.Vector = rijndael.IV;
                        return rijndael.CreateEncryptor();
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        TripleDES edes = new TripleDESCryptoServiceProvider();
                        edes.Mode = CipherMode.CBC;
                        if (this.bWithKey)
                        {
                            edes.Key = bytes;
                        }
                        this.Vector = edes.IV;
                        return edes.CreateEncryptor();
                    }
            }
            throw new CryptographicException("Algorithm " + this.AlgoritmID + " Not Supported!");
        }

        public static bool ValidateKeySize(EncryptionAlgorithm algID, int Lenght)
        {
            switch (algID)
            {
                case EncryptionAlgorithm.DES:
                    {
                        DES des = new DESCryptoServiceProvider();
                        return des.ValidKeySize(Lenght);
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        RC2 rc = new RC2CryptoServiceProvider();
                        return rc.ValidKeySize(Lenght);
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        Rijndael rijndael = new RijndaelManaged();
                        return rijndael.ValidKeySize(Lenght);
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        TripleDES edes = new TripleDESCryptoServiceProvider();
                        return edes.ValidKeySize(Lenght);
                    }
            }
            throw new CryptographicException("Algorithm " + algID + " Not Supported!");
        }

        // Properties
        public EncryptionAlgorithm EncryptionAlgorithm
        {
            get
            {
                return this.AlgoritmID;
            }
            set
            {
                this.AlgoritmID = value;
            }
        }
    }
    public static class EncryptMaker
    {
        // Methods
        public static string Boring(string st)
        {
            for (int i = 0; i < st.Length; i++)
            {
                int startIndex = i * Convert.ToUInt16(st[i]);
                startIndex = startIndex % st.Length;
                char ch = st[i];
                st = st.Remove(i, 1);
                st = st.Insert(startIndex, ch.ToString());
            }
            return st;
        }

        public static string ConvertToLetterDigit(string st)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in st)
            {
                if (!char.IsLetterOrDigit(ch))
                {
                    builder.Append(Convert.ToInt16(ch).ToString());
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        private static char ChangeChar(char ch, int[] EnCode)
        {
            ch = char.ToUpper(ch);
            if ((ch >= 'A') && (ch <= 'H'))
            {
                return Convert.ToChar((int)(Convert.ToInt16(ch) + (2 * EnCode[0])));
            }
            if ((ch >= 'I') && (ch <= 'P'))
            {
                return Convert.ToChar((int)(Convert.ToInt16(ch) - EnCode[2]));
            }
            if ((ch >= 'Q') && (ch <= 'Z'))
            {
                return Convert.ToChar((int)(Convert.ToInt16(ch) - EnCode[1]));
            }
            if ((ch >= '0') && (ch <= '4'))
            {
                return Convert.ToChar((int)(Convert.ToInt16(ch) + 5));
            }
            if ((ch >= '5') && (ch <= '9'))
            {
                return Convert.ToChar((int)(Convert.ToInt16(ch) - 5));
            }
            return '0';
        }

        public static string InverseByBase(string st, int MoveBase)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < st.Length; i += MoveBase)
            {
                int num;
                if ((i + MoveBase) > (st.Length - 1))
                {
                    num = st.Length - i;
                }
                else
                {
                    num = MoveBase;
                }
                builder.Append(InverseString(st.Substring(i, num)));
            }
            return builder.ToString();
        }

        public static string InverseString(string st)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = st.Length - 1; i >= 0; i--)
            {
                builder.Append(st[i]);
            }
            return builder.ToString();
        }

        public static string MakePassword(string st, string Identifier)
        {
            if (Identifier.Length != 3)
            {
                throw new ArgumentException("Identifier must be 3 character length");
            }
            int[] enCode = new int[] { Convert.ToInt32(Identifier[0].ToString(), 10), Convert.ToInt32(Identifier[1].ToString(), 10), Convert.ToInt32(Identifier[2].ToString(), 10) };
            st = Boring(st);
            st = InverseByBase(st, enCode[0]);
            st = InverseByBase(st, enCode[1]);
            st = InverseByBase(st, enCode[2]);
            StringBuilder builder = new StringBuilder();
            foreach (char ch in st)
            {
                builder.Append(ChangeChar(ch, enCode));
            }
            return builder.ToString();
        }
    }
    public class Encryptor
    {
        // Fields
        private EncryptEngine engin;
        public byte[] IV;

        // Methods
        public Encryptor(EncryptionAlgorithm algID, string key)
        {
            this.engin = new EncryptEngine(algID, key);
        }

        public string Encrypt(string MainString)
        {
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, this.engin.GetCryptTransform(), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(stream2);
            writer.WriteLine(MainString);
            writer.Close();
            stream2.Close();
            this.IV = this.engin.Vector;
            byte[] inArray = stream.ToArray();
            stream.Close();
            return Convert.ToBase64String(inArray);
        }

        // Properties
        public EncryptEngine EncryptEngine
        {
            get
            {
                return this.engin;
            }
            set
            {
                this.engin = value;
            }
        }
    }
    public class MyEncryption
    {
        // Methods
        private string byteArrayToString(byte[] inputArray)
        {
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                builder.Append(inputArray[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public string ByteArrayToString(byte[] arryInput)
        {
            StringBuilder builder = new StringBuilder(arryInput.Length);
            for (int i = 0; i < arryInput.Length; i++)
            {
                builder.Append(arryInput[i].ToString());
            }
            return builder.ToString();
        }

        public static string Decrypt(string toDecrypt, string key, bool useHashing)
        {
            byte[] bytes;
            byte[] buffer3;
            if (toDecrypt == "")
            {
                return "";
            }
            byte[] inputBuffer = Convert.FromBase64String(toDecrypt);
            if (useHashing)
            {
                bytes = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider provider3 = new TripleDESCryptoServiceProvider();
            provider3.Key = bytes;
            provider3.Mode = CipherMode.ECB;
            provider3.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = provider3.CreateDecryptor();
            try
            {
                buffer3 = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }
            catch
            {
                throw new ArgumentException("File Data is bad.");
            }
            return Encoding.UTF8.GetString(buffer3);
        }

        public string EncodePassword(string originalPassword)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(originalPassword);
            string str = BitConverter.ToString(md.ComputeHash(bytes));
            str.Replace("-", "");
            return str;
        }

        public static string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            byte[] buffer;
            if (toEncrypt == "")
            {
                return "";
            }
            byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
            if (useHashing)
            {
                buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider provider3 = new TripleDESCryptoServiceProvider();
            provider3.Key = buffer;
            provider3.Mode = CipherMode.ECB;
            provider3.Padding = PaddingMode.PKCS7;
            TripleDESCryptoServiceProvider provider2 = provider3;
            byte[] inArray = provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        private string EncryptString(string strToEncrypt)
        {
            byte[] bytes = new UTF8Encoding().GetBytes(strToEncrypt);
            byte[] buffer2 = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string str = "";
            for (int i = 0; i < buffer2.Length; i++)
            {
                str = str + Convert.ToString(buffer2[i], 0x10).PadLeft(2, '0');
            }
            return str.PadLeft(0x20, '0');
        }

        public static string InverseByBase(string st, int moveBase)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < st.Length; i += moveBase)
            {
                int num2;
                if ((i + moveBase) > (st.Length - 1))
                {
                    num2 = st.Length - i;
                }
                else
                {
                    num2 = moveBase;
                }
                builder.Append(InverseString(st.Substring(i, num2)));
            }
            return builder.ToString();
        }

        public static string InverseString(string st)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = st.Length - 1; i >= 0; i--)
            {
                builder.Append(st[i]);
            }
            return builder.ToString();
        }

        public string Md5Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] inputArray = new MD5CryptoServiceProvider().ComputeHash(encoding.GetBytes(phrase));
            return this.byteArrayToString(inputArray);
        }

        public string Sha1Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] inputArray = new SHA1CryptoServiceProvider().ComputeHash(encoding.GetBytes(phrase));
            return this.byteArrayToString(inputArray);
        }

        public string Sha384Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] inputArray = new SHA384Managed().ComputeHash(encoding.GetBytes(phrase));
            return this.byteArrayToString(inputArray);
        }

        public string Sha512Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] inputArray = new SHA512Managed().ComputeHash(encoding.GetBytes(phrase));
            return this.byteArrayToString(inputArray);
        }

        public string Sha5Encrypt(string phrase)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] inputArray = new SHA256Managed().ComputeHash(encoding.GetBytes(phrase));
            return this.byteArrayToString(inputArray);
        }
    }
}
