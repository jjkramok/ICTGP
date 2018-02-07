/**
 * Adds a new tree to the scene
 * @param x
 * @param y
 * @param z
 * @param scene
 */
function addTree(x, y, z, scene) {
    // Load the 3d object of the tree
    var loader = new THREE.ObjectLoader();
    loader.load("./ModelsAndTextures/tree-05.json", function(object) {
        // Apply materials to all sub-objects
        for (var i = 0; i < object.children.length; i++) {
            object.children[i].material = new THREE.MeshStandardMaterial({
                metalness: 0.6,
                roughness: 0.2,
                color: 0x89f442
            });
        }

        // The tree object is very big, scale it down and add it to the scene.
        object.position.set(x, y, z);
        object.scale.set(0.08, 0.08, 0.08);
        scene.add(object);
    });
}