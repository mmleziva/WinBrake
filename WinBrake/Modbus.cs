using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading;

namespace MB
{
    public struct MB
    {
     //   public bool request;
     //   public bool response;
     //   public WORD length;
        public byte unitId;
        public byte funCode;
        public WORD startAdr;
        public WORD quantity;
        public byte byteCount;
      //  public byte exceptionCode;
        public byte errorCode;
      //  public WORD[] receiveCoilValues;
      //  public WORD[] receiveRegisterValues;
      //  public Int16[] sendRegisterValues;
      //  public bool[] sendCoilValues;
        public WORD crc;
        public ushort words;
    }
    public class Modbus
    {
        static int i;
        const int LH = 128;
        const int LI = 128;
        const int LM = 128;
        public static WORD[] HOLDREGS = new WORD[LH];
        public static WORD[] INPREGS = new WORD[LI];
        public static ushort HOLDADR0 = 40000;
        public static ushort INPADR0 = 30000;
        public static MB ReqStruc, RespStruc,rs;
        public delegate void rec();
        public static event rec MBrec;
        public Modbus()
        {
            ser Ser = new ser();
            ser.Timer.Tick += new EventHandler(Timer_Tick);
        }
        public void Timer_Tick(object sender, EventArgs et)
        {
            try
            {
                B.j = B.i;
                B.i = 0;
                ser.Timer.Stop();
                if (ser.BUSYC)
                {
                    ser.CRCread = ser.calculateCRC(B.bufin, (ushort)B.j, 0);
                    if (ser.CRCread.w == 0)
                    {
                        B.CRCERR = false;
                        B.NO = 0;       //counter of bad received frames
                        rs=Response();//t                  
                    }
                    else
                        B.CRCERR = true;
                    B.j--;
                    MBrec();//event after pause
                    B.j = 0;
                    ser.BUSYC = false;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Modbus timer tick except:\n" + e.ToString());

            }

        }


        //copy  two 2-bytes word registers  into 4-byte single float format 
        public static float ConvRegsToFloat(WORD[] reg, int u)
        {
            byte[] floatBytes = {
                                   reg[u].L,
                                   reg[u].H,
                                   reg[u+1].L,
                                   reg[u+1].H
                                };
            return BitConverter.ToSingle(floatBytes, 0);
        }

        //copy  4-byte single float format into two 2-bytes word registers  
        public static void ConvFloatToRegs( float f, WORD[] reg, int u)
        {
            byte[] b = BitConverter.GetBytes(f);
            reg[u].L = b[0];
            reg[u].H = b[1];
            reg[u + 1].L= b[2];
            reg[u + 1].H = b[3];
        }

        //copy  two 2-bytes word registers  into 4-byte Int32 format 
        public static Int32 ConvRegsToInt32(WORD[] reg, int u)
        {
            byte[] Int32Bytes = {
                                   reg[u].L,
                                   reg[u].H,
                                   reg[u+1].L,
                                   reg[u+1].H
                                };
            return BitConverter.ToInt32(Int32Bytes, 0);
        }

        //copy  4-byte single Int32 format into two 2-bytes word registers  
        public static void ConvInt32ToRegs(Int32 d, WORD[] reg, int u)
        {
            byte[] b = BitConverter.GetBytes(d);
            reg[u].L = b[0];
            reg[u].H = b[1];
            reg[u + 1].L = b[2];
            reg[u + 1].H = b[3];
        }

        //copy  two 2-bytes word registers  into 4-byte Int32 format 
        public static UInt32 ConvRegsToUInt32(WORD[] reg, int u)
        {
            byte[] UInt32Bytes = {
                                   reg[u].L,
                                   reg[u].H,
                                   reg[u+1].L,
                                   reg[u+1].H
                                };
            return BitConverter.ToUInt32(UInt32Bytes, 0);
        }

        //copy  4-byte single UInt32 format into two 2-bytes word registers  
        public static void ConvUInt32ToRegs(UInt32 d, WORD[] reg, int u)
        {
            byte[] b = BitConverter.GetBytes(d);
            reg[u].L = b[0];
            reg[u].H = b[1];
            reg[u + 1].L = b[2];
            reg[u + 1].H = b[3];
        }

        //aux. func: copy modbus registers into  comm. buffer
        public static int WBcopy(WORD[] HREGS, ushort firstadr, byte[] buf, int firstbyte, ushort nums)//copy word into modbus protocol buffer
        {
            i = firstbyte;
            ushort lastadr = firstadr;
            lastadr += nums;
            for (ushort adr = firstadr; adr < lastadr; adr++)
            {
                buf[i] = HREGS[adr].H;
                i++;
                buf[i] = HREGS[adr].L;
                i++;
            }

            return i;
        }

        //aux. func:copy comm. buffer into modbus registers   
        public static int BWcopy(byte[] buf, int firstbyte, WORD[] HREGS, ushort firstadr, ushort nums)
        {
            i = firstbyte;  
            ushort lastadr = firstadr;
            lastadr += nums;
            for (ushort adr = firstadr; adr < lastadr; adr++)
            {
                if(i < B.LB && adr< LM)
                 HREGS[adr].H = buf[i];
                i++;
                if (i < B.LB && adr < LM)
                    HREGS[adr].L= buf[i];
                i++;
            }

            return i;
        }

        //modbus client send frame to server 
        // public static void Require(byte unitId, byte funCode, ushort startAdr, ushort quantity)
        public static void Require(MB ReqStruc)
        {
           try
           { 
            int i = 0;
          //  ReqStruc.unitId = unitId;
            B.bufout[i] = ReqStruc.unitId;
            i++;
          //  ReqStruc.funCode = funCode;
            B.bufout[i] = ReqStruc.funCode;
            i++;
          //  ReqStruc.startAdr.w = startAdr;
          //  ReqStruc.quantity.w = quantity;
            ReqStruc.byteCount = (byte)((ReqStruc.quantity.w & 0x7f) << 1);
            switch (ReqStruc.funCode)
            {
                case (0x3):
                    B.bufout[i] = ReqStruc.startAdr.H;
                    i++;
                    B.bufout[i] = ReqStruc.startAdr.L;
                    i++;
                    B.bufout[i] = ReqStruc.quantity.H;
                    i++;
                    B.bufout[i] = ReqStruc.quantity.L;
                    i++;
                    break;
                case (0x4):
                    B.bufout[i] = ReqStruc.startAdr.H;
                    i++;
                    B.bufout[i] = ReqStruc.startAdr.L;
                    i++;
                    B.bufout[i] = ReqStruc.quantity.H;
                    i++;
                    B.bufout[i] = ReqStruc.quantity.L;
                    i++;
                    break;
                case (0x10):
                    B.bufout[i] = ReqStruc.startAdr.H;
                    i++;
                    B.bufout[i] = ReqStruc.startAdr.L;
                    i++;
                    B.bufout[i] = ReqStruc.quantity.H;
                    i++;
                    B.bufout[i] = ReqStruc.quantity.L;
                    i++;
                    B.bufout[i] = ReqStruc.byteCount;
                    i++;
                    i = WBcopy(HOLDREGS, (ushort)(ReqStruc.startAdr.w- HOLDADR0), B.bufout, i, ReqStruc.quantity.w);
                    break;
            }
            WORD CRC = ser.calculateCRC(B.bufout, (ushort)i, 0);
            B.bufout[i] = CRC.L;
            i++;
            B.bufout[i] = CRC.H;
            i++;
            CRC = ser.calculateCRC(B.bufout, (ushort)i, 0);     //test
            ser.Write(B.bufout, i);
         }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Require except:\n"+ ex.ToString());

            }

        }

        ///modbus client has accepted frame from server
        public static MB Response()
        {
            try
            {
                int i = 0;
                ushort u;
                RespStruc.unitId = B.bufin[i];
                i++;
                RespStruc.errorCode = 0;
                RespStruc.funCode = B.bufin[i];
                i++;
                switch (RespStruc.funCode)
                {
                    case (0x3):
                        RespStruc.startAdr = ReqStruc.startAdr;
                        RespStruc.byteCount = B.bufin[i];
                        i++;
                        u = RespStruc.byteCount;
                        RespStruc.words = (ushort)(RespStruc.byteCount);
                        RespStruc.words >>= 1;
                        i = BWcopy(B.bufin, i, HOLDREGS, (ushort)(RespStruc.startAdr.w - HOLDADR0), RespStruc.words);
                        break;
                    case (0x4):
                        RespStruc.startAdr = ReqStruc.startAdr;
                        RespStruc.byteCount = B.bufin[i];
                        i++;
                        u = RespStruc.byteCount;
                        RespStruc.words = (ushort)(RespStruc.byteCount);
                        RespStruc.words >>= 1;
                        i = BWcopy(B.bufin, i, INPREGS, (ushort)(RespStruc.startAdr.w - INPADR0), RespStruc.words);
                        break;
                    case (0x10):
                        RespStruc.startAdr.H = B.bufin[i];
                        i++;
                        RespStruc.startAdr.L = B.bufin[i];
                        i++;
                        RespStruc.quantity.H = B.bufin[i];
                        i++;
                        RespStruc.quantity.L = B.bufin[i];
                        i++;
                        break;
                    default:
                        if((RespStruc.funCode & 0x80) !=0)
                        {
                            RespStruc.errorCode= B.bufin[i];
                            i++;
                        }

                        break;
                }
                RespStruc.crc.H= B.bufin[i];
                i++;
                RespStruc.crc.L = B.bufin[i];
                i++;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Modbus response except:\n"+ ex.ToString());

            }
            return RespStruc;

        }


    }

}
