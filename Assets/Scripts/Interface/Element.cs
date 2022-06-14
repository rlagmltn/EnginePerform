// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Interface Element.
    /// </summary>
    public abstract class Element : MonoBehaviour
    {
        #region UNITY
        protected virtual void Awake()
        {

        }
        #endregion

        #region METHODS

        protected virtual void Tick() {}

        #endregion
    }
}