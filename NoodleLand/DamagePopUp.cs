using NoodleLand.Entities;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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

[RequireComponent(typeof(TextMeshPro))]
public class DamagePopUp : Entity
{
    private TextMeshPro _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }


    void Animate()
    {
        float randomX = transform.position.x + Random.Range(-2, 2);
        float randomY = transform.position.y + Random.Range(-2, 2);
        
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 1f);
        LeanTween.move(gameObject, new Vector2(randomX, randomY), 1f).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void Set(int amount)
    {
        _textMeshPro.text = amount.ToString();
        Animate();

    }
}
