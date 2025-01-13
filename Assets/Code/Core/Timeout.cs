using System;
using System.Collections;
using UnityEngine;

public static class Timeout
{
    public static IEnumerator TimeStep(WaitForSecondsRealtime delay, bool isLoop, Action callback) {
        do { yield return TimeDelay(delay, callback); } while (isLoop);
    }
    public static IEnumerator TimeDelay(WaitForSecondsRealtime delay, Action callback) {
        yield return delay; callback?.Invoke();
    }

    public static bool IsExpirationDate(string id) => IsExpirationDate(id, out DateTime time);
    public static bool IsExpirationDate(string id, out DateTime savedTime)
    {
        string date = PlayerPrefs.GetString($"{id}_timeout");
        savedTime = string.IsNullOrEmpty(date) ? DateTime.Now : DateTime.Parse(date);
        return savedTime <= DateTime.Now;
    }

    public static int GetExpirationLeftSeconds(string id)
    {
        bool hasExpired = IsExpirationDate(id, out DateTime savedTime);
        return hasExpired ? 0 : (DateTime.Now - savedTime).Seconds;
    }
    public static int GetExpirationLeftHours(string id)
    {
        bool hasExpired = IsExpirationDate(id, out DateTime savedTime);
        return hasExpired ? 0 : (DateTime.Now - savedTime).Hours;
    }

    public static int GetExpirationLengthMinutes(string id)
    {
        bool hasExpired = IsExpirationDate(id, out DateTime savedTime);
        return !hasExpired ? 0 : (DateTime.Now - savedTime).Minutes;
    }
    public static int GetExpirationLengthDays(string id)
    {
        bool hasExpired = IsExpirationDate(id, out DateTime savedTime);
        return !hasExpired ? 0 : (DateTime.Now - savedTime).Days;
    }

    public static void SetExpirationDate(string id) => SetExpirationDate(id, 1);
    public static void SetExpirationDate(string id, int hours) => SetExpirationDate(id, hours, -1);
    public static void SetExpirationDate(string id, int hours, int minutes) => SaveExpirationDate(id, DateTime.Now.AddHours(hours).AddMinutes(minutes));

    public static void SaveExpirationDate(string id) => SaveExpirationDate(id, DateTime.Now);
    public static void SaveExpirationDate(string id, DateTime time) => PlayerPrefs.SetString($"{id}_timeout", time.ToString());
}