#include <windows.h>
#include <wlanapi.h>
#include <iostream>
using namespace std;

 
void main()
{
	
	/*system("netsh wlan show hostednetwork");
	system("pause");*/

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
	/*	char macAddr[30];
		sprintf(macAddr, "%02x%02x%02x%02x%02x%02x",
			(unsigned char)status->PeerList[0].PeerMacAddress[0],
			(unsigned char)status->PeerList[0].PeerMacAddress[1],
			(unsigned char)status->PeerList[0].PeerMacAddress[2],
			(unsigned char)status->PeerList[0].PeerMacAddress[3],
			(unsigned char)status->PeerList[0].PeerMacAddress[4],
			(unsigned char)status->PeerList[0].PeerMacAddress[5]
			);
		cout << macAddr;*/
		for (int n = 0; n < 6; n++)
			cout <<hex<<(int) status->PeerList[0].PeerMacAddress[n];
	}
	system("pause");
}