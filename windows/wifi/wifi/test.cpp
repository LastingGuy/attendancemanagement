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


// ����internet ���ӹ���(ICS)
HRESULT AddSharingNet(INetSharingManager* pNSM)
{


	INetConnection * pNC = NULL;


	INetSharingEveryConnectionCollection * pNSECC = NULL;
	// ö���豸(���������ӣ������������ӡ�����)
	HRESULT hr = pNSM->get_EnumEveryConnection(&pNSECC);
	if (!pNSECC)
	{
		wprintf(L"failed to get EveryConnectionCollection!\r\n");
	}
	else // ��֮��ȡ ö���б�ɹ� 
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
			VariantInit(&v);// ��ʼ�� ���� ����VARIANT���Ǵ���ɲ�׽��
			BOOL bFoundIt = FALSE;
			INetSharingConfiguration * pNSC = NULL;
			INetSharingConfiguration * pVWifiNSC = NULL;
			NETCON_PROPERTIES* pVWifi = NULL;
			while (S_OK == pEV->Next(1, &v, NULL))// ö�������е�Ԫ�� �����ش���ֵ
			{
				if (V_VT(&v) == VT_UNKNOWN)// ����λ������
				{
					V_UNKNOWN(&v)->QueryInterface(__uuidof(INetConnection), (void**)&pNC);  // ��ѯ�豸�Ƿ�֧�ֽӿ�
					if (pNC)
					{
						NETCON_PROPERTIES* pNP = NULL;
						pNC->GetProperties(&pNP);// ��ȡ�豸����
						setlocale(LC_ALL, "chs");
						wprintf(L"pszwName--%s\n", pNP->pszwName);
						wprintf(L"pszwDeviceName--%s\n", pNP->pszwDeviceName);
						wprintf(L"Status--%d\n", pNP->Status);// ���ֵ�Ǳ�ʾ�����Ƿ����
						wprintf(L"MediaType--%d\n", pNP->MediaType);
						wprintf(L"dwCharacter--%d\n", pNP->dwCharacter);
						wprintf(L"clsidThisObject--%d\n", pNP->clsidThisObject);
						wprintf(L"clsidUiObject--%d\n", pNP->clsidUiObject);
						wprintf(L"\n");


						//  ���������������Ϊ��������
						USES_CONVERSION;
						std::string  str(W2A(pNP->pszwDeviceName));
						/* ��������Ϊ�˿�һ����������磨һ������϶����б������ӣ�����������������������Ҫ����wifi�ȵ㣬 ���������磩*/
						if (pNP->Status == NCS_CONNECTED && (-1 == str.find("Virtual")))
						{
							hr = pNSM->get_INetSharingConfigurationForINetConnection(pNC, &pNSC);
							hr = pNSC->EnableSharing(ICSSHARINGTYPE_PUBLIC);
							pNSC->Release();
						}

						/* ���������������ã���������������������綼���ԣ�ǰ�������������Ҫ���������������ģ������Ǹ����ǹ��������������*/
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
			// �������Ҫ ����Ҫ��һ�� ���������  ���û����ô���ָ��Ϊ��
			if (pVWifiNSC == NULL)
			{
				cout << "pVWifiNSC NULL" << endl;
				return -1;
			}


			INetSharingPortMapping * pNSPM = NULL;
			BSTR pszwName = pVWifi->pszwName;
			wprintf(L"BSTR--------------%s\n", pszwName);    // �����õ���������
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
	// ��ӹ�����
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


	// ���ó�������ģʽΪ����
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


	// ���ó��������SSID�����������
	WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS wlanConnectionSetting;
	memset(&wlanConnectionSetting, 0, sizeof(WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS));


	string strSSIDInsert;
	cout << "�����ȵ����ƣ�";
	cin >> strSSIDInsert;
	CString strSSID(strSSIDInsert.c_str());


	// WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS�е�SSID�ֶα���ΪANSI���ͣ�����������ʹ�õ���Unicode������Ҫ��ת����
#ifdef _UNICODE
	WideCharToMultiByte(CP_ACP,
		0,
		strSSID.GetBuffer(0),   // ����SSID��CString����
		strSSID.GetLength(),    // SSID�ַ����ĳ���
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
	cout << "�����ȵ����룺";
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
	// ��������
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
	// ���ó�����������
	dwReturnValue = WlanHostedNetworkStartUsing(hClient, &dwFailedReason, NULL);
	if (ERROR_SUCCESS != dwReturnValue)
	{
		if (wlan_hosted_network_reason_interface_unavailable == dwFailedReason)
		{
			cout << "���߳����������" << endl;
			return 0;
		}
		cout << "δ֪ʧ��" << endl;
		return -1;
	}
	//  ֹͣSoft AP
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
	// // ���ó�������
	dwReturnValue = WlanHostedNetworkForceStart(hClient, &dwFailedReason, NULL);
	if (ERROR_SUCCESS != dwReturnValue)
	{
		cout << "ri" << endl;
		return -1;
	}




	system("pause");


	return 0;
}