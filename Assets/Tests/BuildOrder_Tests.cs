using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;

namespace Tests
{
    public class Test_BuildOrder
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Test_BuildOrderDoesNotHaveActiveDevScenes_ReturnsTrue()
        {
            var activeDevScenes = new List<string>();
        
            foreach(var scene in EditorBuildSettings.scenes)
            {
                if(scene.enabled && scene.path.Contains("/Dev/"))
                    activeDevScenes.Add(scene.path);
            }
        
            Assert.IsTrue(activeDevScenes.Count == 0);
        }
    }
}
