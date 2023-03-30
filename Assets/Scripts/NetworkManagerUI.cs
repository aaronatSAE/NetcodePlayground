using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using TMPro;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputFieldIPAddress;
    [SerializeField] private TMP_InputField inputFieldPort;

    //private void OnEnable()
    //{
    //    inputFieldIPAddress.onValueChanged.AddListener(delegate { UpdateConnectionIPAddress(); });
    //    inputFieldPort.onValueChanged.AddListener(delegate { UpdateConnectionPort(); });
    //}

    public void UpdateConnectionIPAddress()
    {
        NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Address = inputFieldIPAddress.text;
    }

    public void UpdateConnectionPort()
    {
        NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Port = ushort.Parse(inputFieldPort.text);
    }
}
