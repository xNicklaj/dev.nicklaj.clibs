
using UnityEngine;
using VInspector;

namespace dev.nicklaj.clibs.gizmosextensions{
    public class SphereGizmoDrawer : GizmoDrawer
    {
        [Foldout("Gizmo Settings")] [SerializeField]
        private bool UseWireframe = false;
        [Min(0)] public float Radius = .1f ;
        [SerializeField] private bool UseOffset = false;
        [ShowIf("UseOffset")] [SerializeField] private Vector3 Offset = Vector3.zero;

        [EndIf]
        [EndFoldout]

        protected override void Draw()
        {
            var origin = UseOffset ? transform.position + Offset : transform.position;
            if (!UseWireframe) Gizmos.DrawSphere(origin, Radius);
            else Gizmos.DrawWireSphere(origin, Radius);
        }

        public float SetRadius(float radius) => Radius = radius;
    }
}
