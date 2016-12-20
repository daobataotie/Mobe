using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace Common
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class GrandDog
    {
        //define the import function  ,CallingConvention=CallingConvention.Cdecl 
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_OpenDog(uint ulFlag, byte* pszProductName, uint* pDogHandle);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_GetDogInfo(uint DogHandle, byte* pHardwareInfo, uint* pulLen);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_GetProductCurrentNo(uint DogHandle, uint* pulProductCurrentNo);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_VerifyPassword(uint DogHandle, byte bPasswordType, string szPassword, byte* pbDegree);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_ChangePassword(uint DogHandle, byte bPasswordType, string szPassword);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_SetKey(uint DogHandle, byte bKeyType, byte* pucIn, uint ulLen);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_EncryptData(uint DogHandle, byte* pucIn, uint ulInLen, byte* pucOut, uint* pulOutLen);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_DecryptData(uint DogHandle, byte* pucIn, uint ulInLen, byte* pucOut, uint* pulOutLen);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_SignData(uint DogHandle, byte* pucIn, uint ulInLen, byte* pucOut, uint* pulOutLen);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_ConvertData(uint DogHandle, byte* pucIn, uint ulInLen, uint* pulResult);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_CheckDog(uint DogHandle);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_GetRandom(uint DogHandle, byte* pucOut, uint ulInLen);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_CreateDir(uint DogHandle, ushort usDirID, uint ulDirSize);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_CreateFile(uint DogHandle, ushort usDirID, ushort usFileID, byte bFiletype, uint ulFileSize);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_DeleteDir(uint DogHandle, ushort usDirID);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_DeleteFile(uint DogHandle, ushort usDirID, ushort usFileID);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_DefragFileSystem(uint DogHandle, ushort usDirID);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_ReadFile(uint DogHandle, ushort usDirID, ushort usFileID, uint ulPos, uint ulLen, byte* pucOut);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_WriteFile(uint DogHandle, ushort usDirID, ushort usFileID, uint ulPos, uint ulLen, byte* pucIn);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_VisitLicenseFile(uint DogHandle, ushort usDirID, ushort usFileID, uint ulReserved);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_ExecuteFile(uint DogHandle, ushort usDirID, ushort usFileID, byte* pucIn, uint ulInlen, byte* pucOut, uint* pulOutlen);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_GetUpgradeRequestString(uint DogHandle, byte* pucBuf, uint* pulLen);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_Upgrade(uint DogHandle, byte* pucUpgrade, uint ulLen);
        [DllImport("RCGrandDogW32.dll", CharSet = CharSet.Ansi)]
        public static unsafe extern uint rc_CloseDog(uint DogHandle);


        public unsafe uint OpenDog(uint ulFlag, byte* pszProductName, uint* pDogHandle)
        {
            return rc_OpenDog(ulFlag, pszProductName, pDogHandle);
        }

        public unsafe uint CloseDog(uint DogHandle)
        {
            return rc_CloseDog(DogHandle);
        }

        public unsafe uint GetDogInfo(uint DogHandle, byte* pHardwareInfo, uint* pulLen)
        {
            return rc_GetDogInfo(DogHandle, pHardwareInfo, pulLen);
        }

        public unsafe uint GetProductCurrentNo(uint DogHandle, uint* pulProductCurrentNo)
        {
            return rc_GetProductCurrentNo(DogHandle, pulProductCurrentNo);
        }

        public unsafe uint VerifyPassword(uint DogHandle, byte bPasswordType, string szPassword, byte* pbDegree)
        {
            return rc_VerifyPassword(DogHandle, bPasswordType, szPassword, pbDegree);
        }

        public unsafe uint ChangePassword(uint DogHandle, byte bPasswordType, string szPassword)
        {
            return rc_ChangePassword(DogHandle, bPasswordType, szPassword);
        }

        public unsafe uint SetKey(uint DogHandle, byte bKeyType, byte* pucIn, uint ulLen)
        {
            return rc_SetKey(DogHandle, bKeyType, pucIn, ulLen);
        }

        public unsafe uint EncryptData(uint DogHandle, byte* pucIn, uint ulInLen, byte* pucOut, uint* pulOutLen)
        {
            return rc_EncryptData(DogHandle, pucIn, ulInLen, pucOut, pulOutLen);
        }

        public unsafe uint DecryptData(uint DogHandle, byte* pucIn, uint ulInLen, byte* pucOut, uint* pulOutLen)
        {
            return rc_DecryptData(DogHandle, pucIn, ulInLen, pucOut, pulOutLen);
        }

        public unsafe uint SignData(uint DogHandle, byte* pucIn, uint ulInLen, byte* pucOut, uint* pulOutLen)
        {
            return rc_SignData(DogHandle, pucIn, ulInLen, pucOut, pulOutLen);
        }

        public unsafe uint ConvertData(uint DogHandle, byte* pucIn, uint ulInLen, uint* pulResult)
        {
            return rc_ConvertData(DogHandle, pucIn, ulInLen, pulResult);
        }

        public unsafe uint CheckDog(uint DogHandle)
        {
            return rc_CheckDog(DogHandle);
        }

        public unsafe uint GetRandom(uint DogHandle, byte* pucOut, uint ulInLen)
        {
            return rc_GetRandom(DogHandle, pucOut, ulInLen);
        }

        public unsafe uint CreateDir(uint DogHandle, ushort usDirID, uint ulDirSize)
        {
            return rc_CreateDir(DogHandle, usDirID, ulDirSize);
        }

        public unsafe uint CreateFile(uint DogHandle, ushort usDirID, ushort usFileID, byte bFiletype, uint ulFileSize)
        {
            return rc_CreateFile(DogHandle, usDirID, usFileID, bFiletype, ulFileSize);
        }

        public unsafe uint DeleteDir(uint DogHandle, ushort usDirID)
        {
            return rc_DeleteDir(DogHandle, usDirID);
        }

        public unsafe uint DeleteFile(uint DogHandle, ushort usDirID, ushort usFileID)
        {
            return rc_DeleteFile(DogHandle, usDirID, usFileID);
        }

        public unsafe uint DefragFileSystem(uint DogHandle, ushort usDirID)
        {
            return rc_DefragFileSystem(DogHandle, usDirID);
        }

        public unsafe uint ReadFile(uint DogHandle, ushort usDirID, ushort usFileID, uint ulPos, uint ulLen, byte* pucOut)
        {
            return rc_ReadFile(DogHandle, usDirID, usFileID, ulPos, ulLen, pucOut);
        }

        public unsafe uint WriteFile(uint DogHandle, ushort usDirID, ushort usFileID, uint ulPos, uint ulLen, byte* pucIn)
        {
            return rc_WriteFile(DogHandle, usDirID, usFileID, ulPos, ulLen, pucIn);
        }

        public unsafe uint VisitLicenseFile(uint DogHandle, ushort usDirID, ushort usFileID, uint ulReserved)
        {
            return rc_VisitLicenseFile(DogHandle, usDirID, usFileID, ulReserved);
        }

        public unsafe uint ExecuteFile(uint DogHandle, ushort usDirID, ushort usFileID, byte* pucIn, uint ulInlen, byte* pucOut, uint* pulOutlen)
        {
            return rc_ExecuteFile(DogHandle, usDirID, usFileID, pucIn, ulInlen, pucOut, pulOutlen);
        }

        public unsafe uint GetUpgradeRequestString(uint DogHandle, byte* pucBuf, uint* pulLen)
        {
            return rc_GetUpgradeRequestString(DogHandle, pucBuf, pulLen);
        }

        public unsafe uint Upgrade(uint DogHandle, byte* pucUpgrade, uint pulLen)
        {
            return rc_Upgrade(DogHandle, pucUpgrade, pulLen);
        }

        public readonly string productName = "GrandDog";
        public readonly string userPassWord = "12345678";
        public uint ulDogHandle = 0;
        public const byte RC_PASSWORDTYPE_USER = 1;
        public const ulong E_RC_VERIFY_PASSWORD_FAILED = 0xA816000C;
        public ushort usDirID = 16128;
        public ushort usFileID = 16;
    }
}
