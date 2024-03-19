using System;
using System.Collections;
using Photon.Pun;
using SL;
using UnityEngine;

public class InitApp : MonoBehaviourPunCallbacks, IPhotonRPC
{
    [SerializeField] PhotonView photonView;
    [SerializeField] private Table table;
    [SerializeField] private RulesOfTitleRoom rulesOfTitleRoom;
    private void Awake()
    {
        rulesOfTitleRoom.GetCanvas().SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
        ServiceLocator.Instance.RegisterService<ISaveData>(new ServiceSaveData());
        ServiceLocator.Instance.RegisterService<IPhotonRPC>(this);
    }

    public void UpdateTable()
    {
        photonView.RPC(nameof(table.UpdateTable), RpcTarget.AllBuffered);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        ServiceLocator.Instance.GetService<IDebug>().Log("Connected to master");
        rulesOfTitleRoom.GetCanvas().SetActive(true);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<IPhotonRPC>();
    }
}

public interface IPhotonRPC
{
    void UpdateTable();
}