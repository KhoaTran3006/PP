using UnityEngine;

namespace PaintingBoard
{
    public class TaskDetail : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _pictureName;
        [SerializeField] private TMPro.TextMeshProUGUI _colors;

        public void Display(BoardConfig.BoardInfo info)
        {
            _pictureName.text = info.Name;
            string s = "";
            for (int i = 0; i < info.PaintColorsNeeded.Count; i++)
            {
                var color = info.PaintColorsNeeded[i];
                s += (i == info.PaintColorsNeeded.Count - 1 ? color : $"{color}, ");
            }
            _colors.text = s;
        }
    }
}