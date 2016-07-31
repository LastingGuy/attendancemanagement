#include <windows.h>
#include <wlanapi.h>
#include <iostream>
using namespace std;

 
void test(int argc,char* argv)
{
	

	DWORD dError = 0;
	DWORD version = 0;
	HANDLE handle = nullptr;
	if (ERROR_SUCCESS != (dError = WlanOpenHandle(
		WLAN_API_VERSION,
		NULL,
		&version,
		&handle
		)))
	{
		return;
	}

	if (WLAN_API_VERSION_MAJOR(version) < WLAN_API_VERSION_MAJOR(WLAN_API_VERSION_2_0))
	{
		cout << "WlanCloseHandle " << endl;
		WlanCloseHandle(handle, NULL);
	}



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
			
			system("netsh wlan set hostednetwork mode=allow ssid=xs key=123123123");
			system("netsh wlan refresh hostednerwork key");
			system("netsh wlan start hostednetwork");
			WlanOpenHandle(WLAN_API_VERSION,NULL,&version,&handle);
			WlanHostedNetworkQueryStatus(handle, &status, NULL);
		}
		for (int i = 0; i < status->dwNumberOfPeers;i++)
			for (int n = 0; n < 6; n++)
				cout <<hex<<(int) status->PeerList[0].PeerMacAddress[n];
	}
	system("pause");
}