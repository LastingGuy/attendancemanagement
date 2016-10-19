#include <windows.h>
#include <wlanapi.h>
#include <iostream>
#include <stdlib.h>
#include <atlstr.h>
#pragma comment(lib, "wlanapi.lib")
#pragma comment(lib,"wlanui.lib")
using namespace std;
//errorcode
#define FAIL  0
#define SUCCESS 1
#define FAIL_OPEN_HANDLE 2
#define VERSION_IS_LOW   3
#define FAIL_TO_SET_ALLOW 4
#define FAIL_TO_SET_SSID  5
#define FAIL_TO_SET_PASS  6
#define FAIL_TO_START_USING 7
#define FAIL_TO_START_NETWORK 8

DWORD createnetwork(HANDLE handle,string ssid, string password, int maxpeer);
void  getClient(HANDLE handle);
void main(int argc, char* argv[])
{
	if (argc <= 1)
		return;

	HANDLE handle = NULL;
	DWORD version = 0;

	if (ERROR_SUCCESS != WlanOpenHandle(WLAN_API_VERSION, NULL, &version, &handle))
	{
		cout << FAIL_OPEN_HANDLE;
		return;
	}

	if (WLAN_API_VERSION_MAJOR(version) < WLAN_API_VERSION_MAJOR(WLAN_API_VERSION_2_0))
	{
		cout << VERSION_IS_LOW;
		return;
	}

	//cout << createnetwork(handle, "热点", "123123123", 100);
	//getClient(handle);
	if (strcmp(argv[1], "-s")==0)
	{	
		//cout << atoi(argv[4]);
		if (argc == 5)
			cout << createnetwork(handle, argv[2], argv[3], atoi(argv[4]));
		else
			cout << FAIL;
	}
	else if (strcmp(argv[1], "-ls")==0)
	{
		getClient(handle);
	}
	else if (strcmp(argv[1], "-q")==0)
	{
		system("netsh wlan stop hostednetwork");
		 
	}
	//system("pause");
}

DWORD createnetwork(HANDLE handle,string ssid, string password, int maxpeer)
{
	//设置mode为allow
	BOOL isallow = TRUE;
	WLAN_HOSTED_NETWORK_REASON reason;
	if (ERROR_SUCCESS != WlanHostedNetworkSetProperty(
		handle, wlan_hosted_network_opcode_enable, sizeof(BOOL), &isallow, &reason, NULL
	))
	{
		//cout << "fail to set allow";
		return FAIL_TO_SET_ALLOW;
	}

	//设置热点
	WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS settings;
	memset(&settings, 0, sizeof(WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS));
	settings.dwMaxNumberOfPeers = maxpeer;
	CString cstr_ssid(ssid.c_str());
#ifdef _UNICODE
	WideCharToMultiByte(CP_ACP,
		0,
		cstr_ssid.GetBuffer(0),
		cstr_ssid.GetLength(),
		(LPSTR)settings.hostedNetworkSSID.ucSSID,
		100,
		NULL,
		NULL);
#else
	memcpy(wlanConnectionSetting.hostedNetworkSSID.ucSSID, strSSID.GetBuffer(0), strlen(strSSID.GetBuffer(0)));
#endif // _UNICODE
	settings.hostedNetworkSSID.uSSIDLength = strlen((const char*)settings.hostedNetworkSSID.ucSSID);
	if (ERROR_SUCCESS != WlanHostedNetworkSetProperty(
		handle, wlan_hosted_network_opcode_connection_settings,
		sizeof(WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS), &settings,&reason,
		NULL
	))
	{
		//cout << "fail to set ssid";
		return FAIL_TO_SET_SSID;
	}


	//设置密码
	CString cstr_pass(password.c_str());
	UCHAR pass[100] = { 0 };
#ifdef _UNICODE
	WideCharToMultiByte(CP_ACP,
		0,
		cstr_pass.GetBuffer(0),   // 保存SSID的CString类型
		cstr_pass.GetLength(),    // SSID字符串的长度
		(LPSTR)pass,
		100,
		NULL,
		NULL);
#else
	memcpy(pass, strSSID.GetBuffer(0), strlen(cstr_pass.GetBuffer(0)));
#endif // _UNICODE
	if (ERROR_SUCCESS != WlanHostedNetworkSetSecondaryKey(
		handle, strlen((const char*)pass)+1,
		pass, true, false, &reason, NULL
	))
	{
		//cout << "fail to set password";
		return FAIL_TO_SET_PASS;
	}

	
	
	//启用设置
	DWORD dwReturnValue = WlanHostedNetworkStartUsing(handle, &reason, NULL);
	if (ERROR_SUCCESS != dwReturnValue)
	{
		
		return FAIL_TO_START_USING;
	}

	

	//启用负载网路
	dwReturnValue = WlanHostedNetworkForceStart(handle, &reason, NULL);
	if (ERROR_SUCCESS != dwReturnValue)
	{
		//cout << "ri" << endl;
		return FAIL_TO_START_NETWORK;
	}



	//获得密码
	//DWORD length = 0;
	//UCHAR* passs ;
	//WLAN_HOSTED_NETWORK_REASON failReason;
	//DWORD dwKeyLength = 0;//key长度
	//unsigned char *pucKeyData = NULL;
	//BOOL bIsPassPhrase, bPersistent;

	//if(ERROR_SUCCESS== WlanHostedNetworkQuerySecondaryKey(handle, &length, &passs, &bIsPassPhrase, &bPersistent, &failReason, NULL))
	//	cout << passs;



	return SUCCESS;
}

void getClient(HANDLE handle)
{
	PWLAN_HOSTED_NETWORK_STATUS status = nullptr;
	DWORD r = WlanHostedNetworkQueryStatus(handle, &status, NULL);

	if (ERROR_SUCCESS != r)
	{
		return;
	}
	else
	{
		//cout<<status->dwNumberOfPeers;
		if (wlan_hosted_network_active != status->HostedNetworkState)
		{
			return;
		}
		for (int i = 0; i < status->dwNumberOfPeers; i++)
		{
			if(i>0)
				cout << endl;
			cout << "<";
			for (int n = 0; n < 6; n++)
				cout << hex << (int)status->PeerList[i].PeerMacAddress[n];
			cout << ">";
		}
	}
}