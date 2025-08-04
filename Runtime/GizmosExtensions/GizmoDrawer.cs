using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using VInspector;

namespace dev.nicklaj.clibs.gizmosextensions{
    public abstract class GizmoDrawer : MonoBehaviour
    {
        [Tooltip("Whether to always show the gizmo, or only when the object or its parent is selected.")]
        public bool OnlyWhenSelected = true;
        public Color Color = Color.white;
        [Foldout("Sync")]
        [Tooltip("Whether to sync the color between all the gizmos present on this gameobject.")]
        public bool SyncColor = true;
        [Tooltip("Whether to sync OnlyWhenSelected between all the gizmos present on this gameobject.")]
        public bool SyncOnlyWhenSelected = true;
        [EndFoldout]
    
        private void OnDrawGizmos()
        {
            if (OnlyWhenSelected) return;
            DrawGizmo();
        }
    
        private void OnDrawGizmosSelected()
        {
            if (!OnlyWhenSelected) return;
            DrawGizmo();
        }
    
        private void DrawGizmo()
        {
            if (!isActiveAndEnabled) return;
            Gizmos.color = Color;
            Draw();
        }
    
        protected abstract void Draw();
       
        #region Sync
        private void SyncGizmosColor()
        {
            var gizmos = GetComponents<GizmoDrawer>();
    
            foreach (var gizmo in gizmos)
            {
                gizmo.SyncColor = SyncColor;
                if(SyncColor) gizmo.Color = Color;
            }
        }
    
        private void SyncGizmosOnlyWhenSelected()
        {
            var gizmos = GetComponents<GizmoDrawer>();
    
            foreach (var gizmo in gizmos)
            {
                gizmo.SyncOnlyWhenSelected = SyncOnlyWhenSelected;
                if(SyncOnlyWhenSelected) gizmo.OnlyWhenSelected = OnlyWhenSelected;
            }
        }
        #endregion
        
        #region OnValueChange
        [OnValueChanged("SyncColor")]
        private void HandleSyncColorChange() => SyncGizmosColor();
        
        [OnValueChanged("Color")]
        private void HandleColorChange() => SyncGizmosColor();
        
        [OnValueChanged("SyncOnlyWhenSelected")]
        private void HandleSyncOnlyWhenSelectedChange() => SyncGizmosOnlyWhenSelected();
        
        [OnValueChanged("OnlyWhenSelected")]
        private void HandleOnlyWhenSelectedChange() => SyncGizmosOnlyWhenSelected();
        #endregion
    }
}