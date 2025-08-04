using VInspector;
using UnityEngine;

namespace dev.nicklaj.clibs.gizmosextensions{
    public class ForwardGizmoDrawer : GizmoDrawer
    {
        [Foldout("Gizmo Settings")] 
        [Min(0f)] public float Length = 1f;
        [EndFoldout]

        protected override void Draw()
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * Length);
        }
        
        public void SetLenght(float lenght) => Length = lenght;
    }
}
