using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace net.unity3d
{
    class Encryption
    {
        public const byte MaxBoardKey = 255;
        Encryption()
        {

        }

        public static UInt32[] uPublicKeys = {
        0x853B9129, 0x3393F600, 0x21E5731D, 0xB6D5ADE2, 0x3C65D332, 0x876A243B, 0xD83DB293, 0x01B26687, 0xC94E45B0, 0x91B4F1C7, 0x386173B9, 0x3D9937FE, 0x23A15827, 0x1A2F1FB7, 0xDB1FC634, 0xD467817E,
	    0x854DBF5F, 0x2AD8AC62, 0x9A84FBE9, 0x6F39E47A, 0x677E685A, 0x0AEBDC5D, 0x8DDCA5FC, 0xF76D41A8, 0x82A2875E, 0x44E52F7C, 0x0EB694D3, 0x959EB9FD, 0xCF648AF0, 0xA08465D4, 0xB6DDD912, 0xB2AAAEAE,
	    0x88B325D6, 0x28C580BB, 0x5E5FC59F, 0xF6AEC02F, 0x25BB4714, 0x24E4C2C5, 0x1E89EA9F, 0x4C07D037, 0x5FEA21EF, 0x1E5EA9C7, 0x50E71776, 0xDC5200BA, 0x2FEC26EC, 0xDEF1F9D7, 0x8C475ECA, 0x0F6CB0EC,
	    0xCEAE04D0, 0x6D9AB249, 0xADB61180, 0x8D738143, 0xB65DAFA0, 0x159514B3, 0xCA81BFBC, 0x41C25574, 0x9E6656A2, 0x5F5FA1E9, 0x3C343BE4, 0x52F74C75, 0x81776A59, 0x13B61A00, 0x9E9E949C, 0x2BEFC779,
	    0x977C9D8C, 0x3A97804E, 0xC7CA1DCB, 0x73C868F5, 0x5AA2E03F, 0x1E3E1467, 0xD1066693, 0x15DD0FA0, 0x815763B7, 0x47295621, 0x14DE405C, 0x38CCDD6E, 0x07479879, 0x7F140990, 0x2B20BCC9, 0x46723493,
	    0x245F2F4A, 0xCEFD2C09, 0xEDDA2BC0, 0xE9EDB486, 0x51CC1A30, 0x7E200122, 0x73561E65, 0x08983EFA, 0x48FC8A6E, 0x17FB09AF, 0x1824661E, 0xCD11F4E5, 0x009BEF8B, 0x624A05C6, 0x730F1490, 0xA136367C,
	    0xB496FB4B, 0x690BF5BA, 0x5D267A9F, 0x2D22A534, 0xDC1A9EB3, 0x757A1B23, 0xF1B42771, 0x5A3423C2, 0x31950A08, 0x0E15F9D3, 0x8646EE6B, 0x5107CF1B, 0xACB3AFCF, 0xFD984EE2, 0xB6ABDE31, 0x7B790D74,
	    0x87613FCE, 0x4C011BA2, 0x59EE4AA9, 0x81A87B41, 0x3ACDAB08, 0x438CA3AA, 0x8A5DC2F7, 0x4CEFFC39, 0x7E6223C4, 0x6CB765CE, 0xA0851781, 0x05EDB14F, 0x4CCF1986, 0x8F3E2424, 0x34325AEC, 0x157DF9B9,
	    0xDD013D13, 0xB51FDF00, 0x2173DB1C, 0x24BE77EC, 0xAB238170, 0x2997D8F7, 0x7E932E37, 0x1D0B0B9D, 0x6EA415E2, 0x712290DF, 0xA51F21A2, 0x2803D7C1, 0x1F306BEE, 0x587DC7CC, 0x2EE6C602, 0xAD823B8D,
	    0xF7B5345A, 0xE6A68014, 0xF3F36E3B, 0x57A4D876, 0x705E602A, 0x65D9FA4B, 0x0D94AB72, 0x0DC89030, 0x419A21A3, 0x5E94B746, 0xD5564C0D, 0xFA8982B2, 0x6515E749, 0x9894781B, 0xE30664B1, 0x85C6122F,
	    0x14BD64E4, 0x1FD53E1E, 0x11B04243, 0x589BDE1D, 0xC7BD8976, 0x399449E5, 0x78A379E7, 0x5D64C931, 0x38848645, 0x714F1C44, 0x706AD903, 0xBBC0F360, 0x5EBECDD7, 0x90C37650, 0x93D2723B, 0xDC8BBEDF,
	    0x74590EEF, 0x9EEC595F, 0xBAEA9775, 0x69E1CA23, 0xF2813B94, 0xE5080605, 0xFEFDD8D6, 0x4B21F8E1, 0x91A3840A, 0xED93FE17, 0xB79907C3, 0xABE7690D, 0x4B6C5BD6, 0x7F4A01AB, 0x7F8B33E0, 0xF20F7FDE,
	    0x58C971BD, 0xA52C1116, 0x2EDFAE12, 0xC9B8DAC7, 0x30E8B6C5, 0xA77370EB, 0xDFE40980, 0x193E5C7F, 0x8E355C31, 0x0F9E9D01, 0xE925168D, 0x0B3E25F9, 0x6B5ED388, 0xA56A5A6D, 0xE570E4DE, 0x0895966B,
	    0xFE4ECD8E, 0x73D3A783, 0xAED1C659, 0xB95F504A, 0xC2343A48, 0xC117C7D8, 0x5B964B23, 0x06FB354A, 0x6F7C4CFB, 0x19B23942, 0x464D46A1, 0x1A056562, 0xFED4742C, 0x4362BFD5, 0x07C1C777, 0x5D5A42C6,
	    0xA82762A0, 0x48235AE6, 0x79FF1F8B, 0x77176BEA, 0xE7A4085D, 0x72334B0B, 0xB356DF01, 0x5399C385, 0x72B796A6, 0x490E1318, 0x0E51D83F, 0x187D6B8A, 0x440E7E02, 0x97727223, 0x25BE1BEA, 0x31A0C32F,
	    0x95947135, 0x655B6B80, 0xCFAAF9E6, 0x451F6CE9, 0xDF785F44, 0xFA073DC4, 0x26610359, 0x3F57476D, 0xD9277A74, 0xE1F36AC5, 0x82710AA8, 0x46E576B0, 0x7E4C324B, 0xE3DAB297, 0x7DA72077
        };

        public static byte byNumOfPubKeys = 255;


        public static byte rand_key_index()
        {
            //UInt64 s_holdrand = (UInt64)time(null);
            UInt64 s_holdrand = (UInt64)DateTime.Now.Second;

            s_holdrand = s_holdrand * 214013L + 2531011L;
            byte key_index = (byte)(s_holdrand %(MaxBoardKey) + 1);
            //assert(0 != key_index);
            return key_index;
        }

        public static int encode_decode(UInt32 uSize, byte[] pbyBuf, byte puKey)
        {
            List<UInt32> uIntBuff = new List<UInt32>();
            for (int i = 0; i <= pbyBuf.Length - 4; i += 4 )
            {
                UInt32 temp1 = (UInt32)pbyBuf[i];
                UInt32 temp2 = (UInt32)pbyBuf[i+1];
                UInt32 temp3 = (UInt32)pbyBuf[i+2];
                UInt32 temp4 = (UInt32)pbyBuf[i+3];
                UInt32 tempInt = 0;
                tempInt ^= temp1;
                tempInt ^= temp2<<8;
                tempInt ^= temp3<<16;
                tempInt ^= temp4<<24;

                uIntBuff.Add(tempInt);
            }
            //UInt32[] puBuf = (UInt32[])pbyBuf;
            UInt32 uKey = (UInt32)puKey;
            UInt32 uRemainSize = uSize % 4;
            uSize /= 4;

            ///*std::cout<<"puKey"<<uKey<<"pbyBuf:"<<pbyBuf;*/
            for (int i = 0; i < uIntBuff.Count; i++)
            {
                uSize--;
                uKey = uPublicKeys[(uSize + uKey) % byNumOfPubKeys] + 778904513;
                uIntBuff[i] ^= uKey;
            }

            List<Byte> byteBuff = new List<Byte>();
            byte[] tempIntBuff = new byte[4];
            for (int i = 0; i < uIntBuff.Count; i++)
            {
                UInt32 tempInt = uIntBuff[i];
                tempIntBuff[3] = (byte)((tempInt >> 24) & 255);
                tempIntBuff[2] = (byte)((tempInt >> 16) & 255);

                tempIntBuff[1] = (byte)((tempInt >> 8) & 255);
                tempIntBuff[0] = (byte)((tempInt) & 255);
                byteBuff.Add(tempIntBuff[0]);
                byteBuff.Add(tempIntBuff[1]);
                byteBuff.Add(tempIntBuff[2]);
                byteBuff.Add(tempIntBuff[3]);
            }

            uKey ^= uPublicKeys[uRemainSize % byNumOfPubKeys];
            for (int i = 0; i < uRemainSize; i++ )
            {
                pbyBuf[uIntBuff.Count * 4 + i] ^= (byte)(uKey & 0xff);
                uKey >>= 8;
                byteBuff.Add(pbyBuf[uIntBuff.Count * 4 + i]);
            }
            byteBuff.CopyTo(pbyBuf);

            return 0;
        }

    }
}
