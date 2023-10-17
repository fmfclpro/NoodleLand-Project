using System.Collections;
using NoodleLand.Data.Achievements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
MIT License

Copyright (c) 2023 Filipe Lopes | FMFCLPRO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace NoodleLand.Achievements
{
  public class AchievementCompletedPopUp : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI achievementNameText;
    [SerializeField] private Image achievementIconImage;
    [SerializeField] private Vector3 moveTo;
    
    private Vector3 _originalPos;

    private void Start()
    {
      RectTransform tr = (RectTransform) transform;

      _originalPos = tr.anchoredPosition3D;

    }

    void Animate()
    {
      RectTransform tr = (RectTransform) transform;

      LeanTween.move(tr, moveTo, 1).setOnComplete(() =>
      {
        StartCoroutine(MoveBack(tr));
      });
    }

    IEnumerator MoveBack(RectTransform tr)
    {
      yield return new WaitForSeconds(3f);
      LeanTween.move(tr, _originalPos, 3f).setOnComplete(() =>
      {
        gameObject.SetActive(false);
      });
    }
    
    public void Set(Achievement achievement)
    {
      gameObject.SetActive(true);
      this.achievementNameText.text = achievement.AchievementName;
      this.achievementIconImage.sprite = achievement.Image;
      Animate();  
    }
  }
}
