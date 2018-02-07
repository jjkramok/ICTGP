/**
 * Adds a new tree to the scene
 * @param x
 * @param y
 * @param z
 * @param scene
 */
function addTree(x, y, z, scene) {
    var loader = new THREE.ObjectLoader();
    loader.load("./ModelsAndTextures/tree-05.json", function(object) {

        object.position.set(x, y, z);
        object.scale.set(0.08, 0.08, 0.08);
        //object.matrix.makeScale(0.01, 0.01, 0.01);
        //object.updateMatrix();
        scene.add(object);
    });
}