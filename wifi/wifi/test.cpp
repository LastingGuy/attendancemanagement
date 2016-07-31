#include <windows.h>
#include <wlanapi.h>
#include <objbase.h>
#include <wtypes.h>
#include <string>
#include <stdio.h>
#include <stdlib.h>
#include <tchar.h>
// Need to link with Wlanapi.lib and Ole32.lib
#pragma comment(lib, "wlanapi.lib")
#pragma comment(lib, "ole32.lib")
#include <atlconv.h>
#include <iostream>
using namespace std;


#include <atlstr.h>
#include <netcon.h>
#include <NetCon.h>
#include <locale.h>
#include <WinNetWk.h>
#include <netlistmgr.h>
#include <ShObjIdl.h>
#pragma comment (lib,"Mpr.lib")
#pragma comment(lib, "oleaut32.lib")


// 设置internet 连接共享(ICS)
HRESULT AddSharingNet(INetSharingManager* pNSM)
{


	INetConnection * pNC = NULL;


	INetSharingEveryConnectionCollection * pNSECC = NULL;
	// 枚举设备(即本地连接，无线网络连接。。。)
	HRESULT hr = pNSM->get_EnumEveryConnection(&pNSECC);
	if (!pNSECC)
	{
		wprintf(L"failed to get EveryConnectionCollection!\r\n");
	}
	else // 反之获取 枚举列表成功 
	{
		IEnumVARIANT * pEV = NULL;
		IUnknown * pUnk = NULL;
		hr = pNSECC->get__NewEnum(&pUnk);
		if (pUnk)
		{
			hr = pUnk->QueryInterface(__uuidof(IEnumVARIANT), (void**)&pEV);
			pUnk->Release();
		}


		if (pEV)
		{
			VARIANT v;
			VariantInit(&v);// 初始化 错误 类型VARIANT（是错误可捕捉）
			BOOL bFoundIt = FALSE;
			INetSharingConfiguration * pNSC = NULL;
			INetSharingConfiguration * pVWifiNSC = NULL;
			NETCON_PROPERTIES* pVWifi = NULL;
			while (S_OK == pEV->Next(1, &v, NULL))// 枚举序列中的元素 ，返回错误值
			{
				if (V_VT(&v) == VT_UNKNOWN)// 返回位置类型
				{
					V_UNKNOWN(&v)->QueryInterface(__uuidof(INetConnection), (void**)&pNC);  // 查询设备是否支持接口
					if (pNC)
					{
						NETCON_PROPERTIES* pNP = NULL;
						pNC->GetProperties(&pNP);// 获取设备属性
						setlocale(LC_ALL, "chs");
						wprintf(L"pszwName--%s\n", pNP->pszwName);
						wprintf(L"pszwDeviceName--%s\n", pNP->pszwDeviceName);
						wprintf(L"Status--%d\n", pNP->Status);// 这个值是表示网络是否禁用
						wprintf(L"MediaType--%d\n", pNP->MediaType);
						wprintf(L"dwCharacter--%d\n", pNP->dwCharacter);
						wprintf(L"clsidThisObject--%d\n", pNP->clsidThisObject);
						wprintf(L"clsidUiObject--%d\n", pNP->clsidUiObject);
						wprintf(L"\n");


						//  被共享的网络设置为公开共享
						USES_CONVERSION;
						std::string  str(W2A(pNP->pszwDeviceName));
						/* 这里我是为了开一个虚拟的网络（一般电脑上都会有本地连接，无线网络这两个，而我需要设置wifi热点， 即承载网络）*/
						if (pNP->Status == NCS_CONNECTED && (-1 == str.find("Virtual")))
						{
							hr = pNSM->get_INetSharingConfigurationForINetConnection(pNC, &pNSC);
							hr = pNSC->EnableSharing(ICSSHARINGTYPE_PUBLIC);
							pNSC->Release();
						}

						/* 虚拟无线网络设置（本地网络或者是无线网络都可以，前提是这个网络需要是能连接上外网的，上面那个就是共享的它的外网）*/
						if (pNP->Status == NCS_CONNECTED && (-1 != str.find("Virtual")))
						{
							pVWifi = pNP;
							hr = pNSM->get_INetSharingConfigurationForINetConnection(pNC, &pVWifiNSC);
							pVWifiNSC->EnableSharing(ICSSHARINGTYPE_PRIVATE);
							wprintf(L"\nIs this VWifi?--%s\n\n", pVWifi->pszwDeviceName);
						}
					}
				}
			}
			// 因设计需要 必须要有一个 虚拟的网络  如果没有那么这个指针为空
			if (pVWifiNSC == NULL)
			{
				cout << "pVWifiNSC NULL" << endl;
				return -1;
			}


			INetSharingPortMapping * pNSPM = NULL;
			BSTR pszwName = pVWifi->pszwName;
			wprintf(L"BSTR--------------%s\n", pszwName);    // 被设置的虚拟网络
			pVWifiNSC->Release();
			if (pNSPM)
			{
				wprintf(L"just added ........................................................!\r\n");
				hr = pNSPM->Enable();
				wprintf(L"just enabled port mapping!\r\n");


				INetSharingPortMappingProps* pMapping = NULL;
				pNSPM->get_Properties(&pMapping);
				BSTR result = NULL;
				long port = 0;
				pMapping->get_Name(&result);
				pMapping->get_ExternalPort(&port);
				wprintf(L"NAMEEEEE::::%s\n", result);
				wprintf(L"exPortttttt::::%d\n", port);
				pNSPM->Release();
			}
		}
	}


	return hr;
}


int test()
{


	//CoInitialize(NULL);
	//CoInitializeSecurity(NULL, -1, NULL, NULL, RPC_C_AUTHN_LEVEL_PKT,
	//	RPC_C_IMP_LEVEL_IMPERSONATE, NULL, EOAC_NONE, NULL);


	//INetSharingManager * pNSM = NULL;
	//HRESULT hr = ::CoCreateInstance(__uuidof(NetSharingManager), NULL, CLSCTX_ALL,
	//	__uuidof(INetSharingManager), (void**)&pNSM);


	//if (!pNSM)
	//{
	//	wprintf(L"failed to create NetSharingManager object\r\n");
	//}
	// 添加共享功能
//	AddSharingNet(pNSM);




	DWORD dError = 0;
	DWORD dwServiceVersion = 0;
	HANDLE hClient = NULL;
	if (ERROR_SUCCESS != (dError = WlanOpenHandle(
		WLAN_API_VERSION,
		NULL,
		&dwServiceVersion,
		&hClient
		)))
	{
		cout << "WlanOpenHandle ret dwError = " << dError << endl;
		return -1;
	}
	// check service version
	if (WLAN_API_VERSION_MAJOR(dwServiceVersion) < WLAN_API_VERSION_MAJOR(WLAN_API_VERSION_2_0))
	{
		cout << "WlanCloseHandle " << endl;
		WlanCloseHandle(hClient, NULL);
	}


	// 设置承载网络模式为允许
	BOOL bIsAllow = TRUE;
	WLAN_HOSTED_NETWORK_REASON dwFailedReason;
	DWORD dwReturnValue = WlanHostedNetworkSetProperty(hClient,
		wlan_hosted_network_opcode_enable,
		sizeof(BOOL),
		&bIsAllow,
		&dwFailedReason,
		NULL);
	if (ERROR_SUCCESS != dwReturnValue)
	{


		cout << "WlanHostedNetworkSetProperty  ret dwReturnValue" << dwReturnValue << endl;
		return -1;
	}


	// dwReturnValue = WlanHostedNetworkStopUsing( hClient, &dwFailedReason, NULL );


	// 设置承载网络的SSID和最大连接数
	WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS wlanConnectionSetting;
	memset(&wlanConnectionSetting, 0, sizeof(WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS));


	string strSSIDInsert;
	cout << "设置热点名称：";
	cin >> strSSIDInsert;
	CString strSSID(strSSIDInsert.c_str());


	// WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS中的SSID字段必须为ANSI类型，因此如果程序使用的是Unicode，则需要做转换。
#ifdef _UNICODE
	WideCharToMultiByte(CP_ACP,
		0,
		strSSID.GetBuffer(0),   // 保存SSID的CString类型
		strSSID.GetLength(),    // SSID字符串的长度
		(LPSTR)wlanConnectionSetting.hostedNetworkSSID.ucSSID,
		32,
		NULL,
		NULL);
#else
	memcpy(wlanConnectionSetting.hostedNetworkSSID.ucSSID, strSSID.GetBuffer(0), strlen(strSSID.GetBuffer(0)));
#endif  
	wlanConnectionSetting.hostedNetworkSSID.uSSIDLength = strlen((const char*)wlanConnectionSetting.hostedNetworkSSID.ucSSID);
	wlanConnectionSetting.dwMaxNumberOfPeers = 100;
	dwReturnValue = WlanHostedNetworkSetProperty(hClient,
		wlan_hosted_network_opcode_connection_settings,
		sizeof(WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS),
		&wlanConnectionSetting,
		&dwFailedReason,
		NULL);
	if (ERROR_SUCCESS != dwReturnValue)
	{
		cout << "WlanHostedNetworkSetProperty ret " << dwReturnValue << endl;
		return -1;
	}

	string strPass("");
	cout << "设置热点密码：";
	cin >> strPass;
	//CString strSecondaryKey = _T( "12345678" );
	CString strSecondaryKey(strPass.c_str());


	UCHAR keyBuf[100] = { 0 };
#ifdef _UNICODE
	WideCharToMultiByte(CP_ACP,
		0,
		strSecondaryKey.GetBuffer(0),
		strSecondaryKey.GetLength(),
		(LPSTR)keyBuf,
		100,
		NULL,
		NULL);
#else
	memcpy(keyBuf, strSecondaryKey.GetBuffer(0), strlen(strSecondaryKey.GetBuffer(0)));
#endif
	// 设置密码
	PVOID  pvReserved = NULL;
	dwReturnValue = WlanHostedNetworkSetSecondaryKey(hClient,
		strlen((const char*)keyBuf) + 1,
		keyBuf,
		TRUE,
		FALSE,
		&dwFailedReason,
		pvReserved);
	if (ERROR_SUCCESS != dwReturnValue)
	{
		if (ERROR_INVALID_PARAMETER == dwReturnValue)
		{
			if (hClient == NULL)
			{
				cout << "hClient == NULL" << endl;
			}
			if (keyBuf == NULL)
			{
				cout << "keyBuf == NULL" << endl;
			}
			if (pvReserved != NULL)
			{
				cout << "pvReserved != NULL" << endl;
			}
			cout << dwReturnValue << "---" << ERROR_INVALID_PARAMETER << endl;
		}
		return -1;
	}
	// 启用承载网络设置
	dwReturnValue = WlanHostedNetworkStartUsing(hClient, &dwFailedReason, NULL);
	if (ERROR_SUCCESS != dwReturnValue)
	{
		if (wlan_hosted_network_reason_interface_unavailable == dwFailedReason)
		{
			cout << "无线承载网络禁用" << endl;
			return 0;
		}
		cout << "未知失败" << endl;
		return -1;
	}
	//  停止Soft AP
	// dwReturnValue = WlanHostedNetworkStopUsing( hClient, &dwFailedReason, NULL );
	// if (ERROR_SUCCESS != dwReturnValue)
	// {
	// if (ERROR_INVALID_HANDLE == dwReturnValue || ERROR_INVALID_PARAMETER == dwReturnValue ||
	// ERROR_INVALID_STATE == dwReturnValue || ERROR_SERVICE_NOT_ACTIVE == dwReturnValue)
	// {
	// cout << dwReturnValue << endl;
	// }
	// 
	// cout << dwReturnValue << endl;
	// return -1;
	// }
	// // 启用承载网络
	dwReturnValue = WlanHostedNetworkForceStart(hClient, &dwFailedReason, NULL);
	if (ERROR_SUCCESS != dwReturnValue)
	{
		cout << "ri" << endl;
		return -1;
	}




	system("pause");


	return 0;
}