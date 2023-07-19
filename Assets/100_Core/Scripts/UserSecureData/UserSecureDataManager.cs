using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public partial class UserSecureDataManager : MonoBehaviour
{
    #region ���� ����

    private string savePath = null;
    private ES3File file;
    private Dictionary<string, string> bufferData = new Dictionary<string, string>();
    public bool IsAvailable => file != null;

    #endregion

    #region ���� ����

    // ���� �����ϱ�
    public void Commit()
    {
        GameUtils.Log("UserSecureDataManager / Commit ()");
        file?.Sync();
    }

    private bool isAwake = false;

    void Awake()
    {
        isAwake = true;
        savePath = PlayerPrefs.GetString("DefDataKey", "arbszPsOCg.es");

        CheckDeleteDataKey();
    }

    private void CheckDeleteDataKey()
    {
        var deleteDataKeyList =
            JsonConvert.DeserializeObject<List<string>>(PlayerPrefs.GetString("DeleteDataKeyList",
                JsonConvert.SerializeObject(new List<string>())));
        if (deleteDataKeyList.Count > 5) // 5�� �ʰ� ���̸� ����
        {
            for (int i = 0; i < deleteDataKeyList.Count; i++)
            {
                ES3.DeleteFile(deleteDataKeyList[i]);
                deleteDataKeyList.RemoveAt(i);
                i--;
            }

            PlayerPrefs.SetString("DeleteDataKeyList", JsonConvert.SerializeObject(deleteDataKeyList));
        }
    }


    public void ResetAllData()
    {
        string beforeSavePath = savePath;

        //ClearUpdateKeyList(); // ��ũ Key����Ʈ Ŭ�����ϱ�

        savePath = $"arbszPsOCg_{DateTime.UtcNow.Ticks.GetHashCode()}.es";
        file = new ES3File(savePath);
        bufferData.Clear();
        PlayerPrefs.SetString("DefDataKey", savePath);

        // �����ؾ� �ϴ� ������ Ű �����س���
        var deleteDataKeyList =
            JsonConvert.DeserializeObject<List<string>>(PlayerPrefs.GetString("DeleteDataKeyList",
                JsonConvert.SerializeObject(new List<string>())));

        if (deleteDataKeyList.Contains(beforeSavePath) == false)
        {
            deleteDataKeyList.Add(beforeSavePath);
            PlayerPrefs.SetString("DeleteDataKeyList", JsonConvert.SerializeObject(deleteDataKeyList));
        }
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }

    public void Init()
    {
        // ������ �ѹ��� ����Ǵ� ����
        if (isAwake == true)
        {
            isAwake = false;
            file = new ES3File(savePath);
            bufferData.Clear();
        }
    }

    void OnApplicationQuit()
    {
        Commit();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == true)
        {
            Commit();
        }
    }

    // ������ ����ε�
    public long GetData_Long(string key, long longDefault)
    {
        string data = GetData(key);
        if (data != null)
        {
            longDefault = long.Parse(data);
        }

        return longDefault;
    }

    // ������ ����ε�
    public double GetData_Double(string key, double doubleDefault)
    {
        string data = GetData(key);
        if (data != null)
        {
            doubleDefault = double.Parse(data);
        }

        return doubleDefault;
    }

    // ������ ����ε�
    public float GetData_Float(string key, float floatDefault)
    {
        string data = GetData(key);
        if (data != null)
        {
            floatDefault = float.Parse(data);
        }

        return floatDefault;
    }


    // ������ ����ε�
    public int GetData_Int(string key, int longDefault)
    {
        string data = GetData(key);
        if (data != null)
        {
            longDefault = int.Parse(data);
        }

        return longDefault;
    }

    public string GetData(string key)
    {
        string valueData = null;

        if (bufferData.ContainsKey(key) == true)
        {
            valueData = bufferData[key];
        }
        else
        {
            try
            {
                if (file.KeyExists(string.Format("USD_{0}", key)) == true)
                {
                    valueData = file.Load<string>(string.Format("USD_{0}", key));
                    bufferData.Add(key, valueData);
                }
            }
            catch (Exception ex)
            {
                GameUtils.LogException(ex);
            }
        }

        return valueData;
    }

    public void SetData(string key, long data, bool allowAddUpdateKeyList = true)
    {
        SetData(key, data.ToString(), allowAddUpdateKeyList);
    }

    public void SetData(string key, double data, bool allowAddUpdateKeyList = true)
    {
        SetData(key, data.ToString(), allowAddUpdateKeyList);
    }

    public void SetData(string key, float data, bool allowAddUpdateKeyList = true)
    {
        SetData(key, data.ToString(), allowAddUpdateKeyList);
    }

    public void SetData(string key, int data, bool allowAddUpdateKeyList = true)
    {
        SetData(key, data.ToString(), allowAddUpdateKeyList);
    }


    public void SetData(string key, string data, bool allowAddUpdateKeyList = true)
    {
        // ������ �������� üũ
        bool isChange = false;

        if (bufferData.ContainsKey(key) == true)
        {
            // �񱳵����� Null�� ����ó�� 
            var checkData_a = bufferData[key];
            var checkData_b = data;
            if (checkData_a == null)
            {
                checkData_a = "";
            }

            if (checkData_b == null)
            {
                checkData_b = "";
            }

            // ������ �������� üũ
            if (checkData_a.Equals(checkData_b) == false)
            {
                isChange = true;
            }

            bufferData[key] = data;
        }
        else
        {
            // �񱳵����� Null�� ����ó�� 
            var checkData_a = GetData(key);
            var checkData_b = data;
            if (checkData_a == null)
            {
                checkData_a = "";
            }

            if (checkData_b == null)
            {
                checkData_b = "";
            }

            // ������ �������� üũ
            if (checkData_a.Equals(checkData_b) == false)
            {
                isChange = true;
            }

            if (bufferData.ContainsKey(key) == true)
            {
                bufferData[key] = data;
            }
            else
            {
                bufferData.Add(key, data);
            }
        }

        try
        {
            if (data?.Length > 5000)
            {
                if ((key.Equals("EquippedArmors_DisplayOnly") == true)
                    || (key.Equals("EquippedRings_DisplayOnly") == true))
                {
                    // Ư��Ű�� ũ�� üũ���� ����
                }
                else
                {
                    GameUtils.Error($"���������� ������ ũ�Ⱑ �ʹ� ŭ! �ݵ�� �����ʿ�! {key} = {data.Length}");
                }
            }

            file.Save<string>(string.Format("USD_{0}", key), data);
        }
        catch (Exception ex)
        {
            GameUtils.LogException(ex);
        }
    }

    #endregion
}
