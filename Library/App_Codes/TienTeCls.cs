using System;

namespace Library
{
    public class TienTeCls
    {
        private string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
        private string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };

        // Hàm đọc số có 3 chữ số
        private string DocSo3ChuSo(int baso)
        {
            int tram, chuc, donvi;
            string KetQua = "";
            tram = (int)(baso / 100);
            chuc = (int)((baso % 100) / 10);
            donvi = baso % 10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
            if (tram != 0)
            {
                KetQua += ChuSo[tram] + " trăm";
                if ((chuc == 0) && (donvi != 0)) KetQua += " linh";
            }
            if ((chuc != 0) && (chuc != 1))
            {
                KetQua += ChuSo[chuc] + " mươi";
                if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " linh";
            }
            if (chuc == 1) KetQua += " mười";
            switch (donvi)
            {
                case 1:
                    if ((chuc != 0) && (chuc != 1))
                    {
                        KetQua += " mốt";
                    }
                    else
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
                case 5:
                    if (chuc == 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    else
                    {
                        KetQua += " lăm";
                    }
                    break;
                default:
                    if (donvi != 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
            }
            return KetQua;
        }
        // Hàm đọc số thành chữ
        public string DocTienBangChu(long SoTien)
        {
            int lan, i;
            long so;
            string KetQua = "", tmp = "";
            int[] ViTri = new int[6];
            if (SoTien < 0) return "Số tiền âm !";
            if (SoTien == 0) return "Không đồng !";
            if (SoTien > 0)
            {
                so = SoTien;
            }
            else
            {
                so = -SoTien;
            }
            //Kiểm tra số quá lớn
            if (SoTien > 8999999999999999)
            {
                SoTien = 0;
                return "";
            }
            ViTri[5] = (int)(so / 1000000000000000);
            so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
            ViTri[4] = (int)(so / 1000000000000);
            so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
            ViTri[3] = (int)(so / 1000000000);
            so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
            ViTri[2] = (int)(so / 1000000);
            ViTri[1] = (int)((so % 1000000) / 1000);
            ViTri[0] = (int)(so % 1000);
            if (ViTri[5] > 0)
            {
                lan = 5;
            }
            else if (ViTri[4] > 0)
            {
                lan = 4;
            }
            else if (ViTri[3] > 0)
            {
                lan = 3;
            }
            else if (ViTri[2] > 0)
            {
                lan = 2;
            }
            else if (ViTri[1] > 0)
            {
                lan = 1;
            }
            else
            {
                lan = 0;
            }
            for (i = lan; i >= 0; i--)
            {
                tmp = DocSo3ChuSo(ViTri[i]);
                KetQua += tmp;
                if (ViTri[i] != 0) KetQua += Tien[i];
                if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
            }
            if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
            KetQua = KetQua.Trim() + " đồng";
            return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
        }

        public string DocTienBangChu(decimal SoTien, string LoaiTien)
        {
            int lan, i;
            long so = Convert.ToInt64(Math.Round(SoTien, 0, MidpointRounding.AwayFromZero));
            string KetQua = "", tmp = "";
            int[] ViTri = new int[6];
            if (so < 0) return "Số tiền âm !";
            if (so == 0) return "Không " + LoaiTien;
            //Kiểm tra số quá lớn
            if (so > 8999999999999999)
            {
                return "";
            }
            ViTri[5] = (int)(so / 1000000000000000);
            so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
            ViTri[4] = (int)(so / 1000000000000);
            so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
            ViTri[3] = (int)(so / 1000000000);
            so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
            ViTri[2] = (int)(so / 1000000);
            ViTri[1] = (int)((so % 1000000) / 1000);
            ViTri[0] = (int)(so % 1000);
            if (ViTri[5] > 0)
            {
                lan = 5;
            }
            else if (ViTri[4] > 0)
            {
                lan = 4;
            }
            else if (ViTri[3] > 0)
            {
                lan = 3;
            }
            else if (ViTri[2] > 0)
            {
                lan = 2;
            }
            else if (ViTri[1] > 0)
            {
                lan = 1;
            }
            else
            {
                lan = 0;
            }
            for (i = lan; i >= 0; i--)
            {
                tmp = DocSo3ChuSo(ViTri[i]);
                KetQua += tmp;
                if (ViTri[i] != 0) KetQua += Tien[i];
                if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
            }
            if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
            KetQua = KetQua.Trim() + " " + LoaiTien;
            return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
        }

        public string DocTienBangChuEN(decimal Input, string LoaiTien)
        {
            //Call this function passing the number you desire to be changed
            string output = null;
            Input = Convert.ToInt64(Input);
            if (Input < 1000)
            {
                output = FindNumber(Input);
                //if its less than 1000 then just look it up
            }
            else
            {
                string[] nparts = null;
                //used to break the number up into 3 digit parts
                string n = Input.ToString();
                //string version of the number
                int i = Input.ToString().Length;
                //length of the string to help break it up

                while (!(i - 3 <= 0))
                {
                    n = n.Insert(i - 3, ",");  
                    //insert commas to use as splitters
                    i = i - 3;
                    //this insures that we get the correct number of parts
                }
                nparts = n.Split(',');
                //split the string into the array

                i = Input.ToString().Length;
                //return i to initial value for reuse
                int p = 0;
                //p for parts, used for finding correct suffix
                foreach (string s in nparts)
                {
                    long x = Convert.ToInt64(s);
                    //x is used to compare the part value to other values
                    p = p + 1;
                    //if p = number of elements in the array then we need to do something different
                    if (p == nparts.Length)
                    {
                        if (x != 0)
                        {
                            if (Convert.ToInt64(s) < 100)
                            {
                                output = output + " And " + FindNumber(Convert.ToInt64(s));
                                // look up the number, no suffix 
                                // required as this is the last part
                            }
                            else
                            {
                                output = output + " " + FindNumber(Convert.ToInt64(s));
                            }
                        }
                        //if its not the last element in the array
                    }
                    else
                    {
                        if (x != 0)
                        {
                            //we have to check this so we don't add a leading space
                            if (output == null)
                            {
                                output = output + FindNumber(Convert.ToInt64(s)) + " " + FindSuffix(i, Convert.ToInt64(s));
                                //look up the number and suffix
                                //spaces must go in the right place
                            }
                            else
                            {
                                output = output + " " + FindNumber(Convert.ToInt64(s)) + " " + FindSuffix(i, Convert.ToInt64(s));
                                //look up the snumber and suffix
                            }
                        }
                    }
                    i = i - 3;
                    //reduce the suffix counter by 3 to step down to the next suffix
                }
            }
            return output.Substring(0, 1).ToUpper() + output.Substring(1, output.Length-1).ToLower() + " " + LoaiTien + "" ;
        }

        private string FindNumber(decimal Number)
        {
            string Words = null;
            string[] Digits = {
			"Zero",
			"One",
			"Two",
			"Three",
			"Four",
			"Five",
			"Six",
			"Seven",
			"Eight",
			"Nine",
			"Ten"
		};
            string[] Teens = {
			"",
			"Eleven",
			"Twelve",
			"Thirteen",
			"Fourteen",
			"Fifteen",
			"Sixteen",
			"Seventeen",
			"Eighteen",
			"Nineteen"
		};

            if (Number < 11)
            {
                Words = Digits[(int)Number];

            }
            else if (Number < 20)
            {
                Words = Teens[(int)Number - 10];

            }
            else if (Number == 20)
            {
                Words = "Twenty";

            }
            else if (Number < 30)
            {
                Words = "Twenty " + Digits[(int)Number - 20];

            }
            else if (Number == 30)
            {
                Words = "Thirty";

            }
            else if (Number < 40)
            {
                Words = "Thirty " + Digits[(int)Number - 30];

            }
            else if (Number == 40)
            {
                Words = "Fourty";

            }
            else if (Number < 50)
            {
                Words = "Fourty " + Digits[(int)Number - 40];

            }
            else if (Number == 50)
            {
                Words = "Fifty";

            }
            else if (Number < 60)
            {
                Words = "Fifty " + Digits[(int)Number - 50];

            }
            else if (Number == 60)
            {
                Words = "Sixty";

            }
            else if (Number < 70)
            {
                Words = "Sixty " + Digits[(int)Number - 60];

            }
            else if (Number == 70)
            {
                Words = "Seventy";

            }
            else if (Number < 80)
            {
                Words = "Seventy " + Digits[(int)Number - 70];

            }
            else if (Number == 80)
            {
                Words = "Eighty";

            }
            else if (Number < 90)
            {
                Words = "Eighty " + Digits[(int)Number - 80];

            }
            else if (Number == 90)
            {
                Words = "Ninety";

            }
            else if (Number < 100)
            {
                Words = "Ninety " + Digits[(int)Number - 90];

            }
            else if (Number < 1000)
            {
                Words = Number.ToString();
                Words = Words.Insert(1, ",");  
                string[] wparts = Words.Split(',');
                Words = FindNumber(int.Parse(wparts[0])) + " " + "Hundred";
                string n = FindNumber(int.Parse(wparts[1]));
                if (Convert.ToInt64(wparts[1]) != 0)
                {
                    Words = Words + " And " + n;
                }
            }

            return Words;
        }

        private string FindSuffix(long Length, long l)
        {
            string word = null;

            if (l != 0)
            {
                if (Length > 12)
                {
                    word = "Trillion";
                }
                else if (Length > 9)
                {
                    word = "Billion";
                }
                else if (Length > 6)
                {
                    word = "Million";
                }
                else if (Length > 3)
                {
                    word = "Thousand";
                }
                else if (Length > 2)
                {
                    word = "Hundred";
                }
                else
                {
                    word = "";
                }
            }
            else
            {
                word = "";
            }

            return word;
        }
    }
}
