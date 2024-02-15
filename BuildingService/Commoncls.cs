using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections;

namespace BuildingService
{
    public class Commoncls
    {
        public static string EncodeString(string inputString)
        {
            CspParameters parameters = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(0x400, parameters);
            CspParameters parameters2 = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider2 = new RSACryptoServiceProvider();
            provider2.FromXmlString("<RSAKeyValue><Modulus>rxZwQi8PwO9vGKVxGFTzuehApb0MpO92N/HOAMe0Ib7VkS6++gDtrFiotHWPzUjUklKa2hJjmG+6Sh74c+iwJpU7dQGRxvoXYuF+m9r4lyGzXTrRP4Wt16SmbF8Pm6jaw9JPu1Xy+8sVBxYq8B5jyI5aaZ7aKvSBuJGLMtv/wcE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);
            int length = bytes.Length;
            int num2 = length / 0x56;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i <= num2; i++)
            {
                byte[] dst = new byte[((length - (0x56 * i)) > 0x56) ? 0x56 : (length - (0x56 * i))];
                Buffer.BlockCopy(bytes, 0x56 * i, dst, 0, dst.Length);
                byte[] array = provider2.Encrypt(dst, true);
                Array.Reverse(array);
                builder.Append(Convert.ToBase64String(array));
            }
            return builder.ToString();
        }

        public static string DecodeString(string inputString)
        {
            CspParameters parameters = new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString("<RSAKeyValue><Modulus>rxZwQi8PwO9vGKVxGFTzuehApb0MpO92N/HOAMe0Ib7VkS6++gDtrFiotHWPzUjUklKa2hJjmG+6Sh74c+iwJpU7dQGRxvoXYuF+m9r4lyGzXTrRP4Wt16SmbF8Pm6jaw9JPu1Xy+8sVBxYq8B5jyI5aaZ7aKvSBuJGLMtv/wcE=</Modulus><Exponent>AQAB</Exponent><P>5nR8EplxlG0uPVGorn8OkMXZ9TF7BPa5wZs1vL4JPsxZv8D+UjufUsGrHOQmZRxvFe4J/1/iZI/6m+nHOcFk1w==</P><Q>wn7R12szMYoIMFN8UEXcEmamO7PSELqhV+qe9a/7N6G1pKG1xU3AZpkfW0E/GJZGl7pA9UQNQZTxS/LSv0AjJw==</Q><DP>inrSl4aXBp6422X3W6vDv+D0AO+Twb7Ujm9K0jjLa232PFCnQhjLuznfLcQ3Aikc42ufnFIsw0r1R70p1x3MDw==</DP><DQ>lYaKLOLtaJiF0yFb4RrUJhFkm2GTjejtQXnO23N/3zUjQH5SEG3GDRqLUMzIhU6C1wMKDYVT66dmGs2D2CSm4Q==</DQ><InverseQ>eXW6RmvwuAoo52IAnv9dBq+ixrZqhDKyFRYusjuUpFggPw7A4OknUNwJtCHeQecOCmKNTo0T+AmGfq530XnDqg==</InverseQ><D>RTclocRhAfClhqTAlNHgl/nMtLiLqxhPL8aTnZNVDpIWc5J7RPHhA2T5LH3dH1ZPUpj9RoBGhxiEGJEtvwSZvb76txmEXaUlou0ZZveeJe7O+crWT70dn06Qz+Ua7F6uwpVCQr7VmTEY4qXFowvrdH8Haz/2uHM+FFpv/1idD9E=</D></RSAKeyValue>");
            int num = inputString.Length / 0xac;
            ArrayList list = new ArrayList();
            for (int i = 0; i < num; i++)
            {
                byte[] array = Convert.FromBase64String(inputString.Substring(0xac * i, 0xac));
                Array.Reverse(array);
                list.AddRange(provider.Decrypt(array, true));
            }
            return Encoding.UTF32.GetString(list.ToArray(Type.GetType("System.Byte")) as byte[]);
        }
        
    }
}
