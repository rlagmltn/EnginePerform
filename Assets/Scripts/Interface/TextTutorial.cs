using TMPro;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Interface component that hides or shows the tutorial text based on input.
    /// </summary>
    public class TextTutorial : ElementText
    {
        #region FIELDS SERIALIZED
        
        [Header("References")]
        
        [Tooltip("Tutorial prompt text.")]
        [SerializeField]
        private TextMeshProUGUI prompt;

        [Tooltip("Tutorial text.")]
        [SerializeField]
        private TextMeshProUGUI tutorial;

        #endregion

        #region UNITY

        protected override void Awake()
        {
            //Base.
            base.Awake();

            prompt.enabled = true;
            tutorial.enabled = true;
        }

        #endregion
    }
}