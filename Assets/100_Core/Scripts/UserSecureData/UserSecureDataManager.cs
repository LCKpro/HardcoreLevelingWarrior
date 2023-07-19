using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public partial class UserSecureDataManager : MonoBehaviour
{
    #region 변수 모음

    private string savePath = null;
    private ES3File file;
    private Dictionary<string, string> bufferData = new Dictionary<string, string>();
    public bool IsAvailable => file != null;

    #endregion

    #region 공통 로직

    // 파일 저장하기
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
        if (deleteDataKeyList.Count > 5) // 5개 초과 쌓이면 삭제
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

        //ClearUpdateKeyList(); // 싱크 Key리스트 클리어하기

        savePath = $"arbszPsOCg_{DateTime.UtcNow.Ticks.GetHashCode()}.es";
        file = new ES3File(savePath);
        bufferData.Clear();
        PlayerPrefs.SetString("DefDataKey", savePath);

        // 삭제해야 하는 데이터 키 저장해놓기
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
        // 생성시 한번만 실행되는 로직
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

    // 데이터 저장로드
    public long GetData_Long(string key, long longDefault)
    {
        string data = GetData(key);
        if (data != null)
        {
            longDefault = long.Parse(data);
        }

        return longDefault;
    }

    // 데이터 저장로드
    public double GetData_Double(string key, double doubleDefault)
    {
        string data = GetData(key);
        if (data != null)
        {
            doubleDefault = double.Parse(data);
        }

        return doubleDefault;
    }

    // 데이터 저장로드
    public float GetData_Float(string key, float floatDefault)
    {
        string data = GetData(key);
        if (data != null)
        {
            floatDefault = float.Parse(data);
        }

        return floatDefault;
    }


    // 데이터 저장로드
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
        // 데이터 변동여부 체크
        bool isChange = false;

        if (bufferData.ContainsKey(key) == true)
        {
            // 비교데이터 Null값 예외처리 
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

            // 데이터 변동여부 체크
            if (checkData_a.Equals(checkData_b) == false)
            {
                isChange = true;
            }

            bufferData[key] = data;
        }
        else
        {
            // 비교데이터 Null값 예외처리 
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

            // 데이터 변동여부 체크
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
                    // 특정키는 크기 체크에서 제외
                }
                else
                {
                    GameUtils.Error($"유저데이터 아이템 크기가 너무 큼! 반드시 수정필요! {key} = {data.Length}");
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
