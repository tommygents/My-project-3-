using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeatUIController : MonoBehaviour
{
    [SerializeField] private BeatManager beatManager;
    [SerializeField] private Image beatImage;
    [SerializeField] private TextMeshProUGUI hitText;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color beatColor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (beatManager.IsInputWithinWindow())
            {
                hitText.text = "GREAT!";
            }
            else
            {
                hitText.text = "MISS!";
            }
        }

        UpdateBeatImageColor();
    }

    private void UpdateBeatImageColor()
    {
        float timeUntilNextBeat = Mathf.Abs(beatManager.NextBeatTime);
        float halfWindowSize = beatManager.WindowSize / 2;

        if (timeUntilNextBeat <= halfWindowSize)
        {
            beatImage.color = beatColor;
        }
        else
        {
            beatImage.color = normalColor;
        }
    }
}
