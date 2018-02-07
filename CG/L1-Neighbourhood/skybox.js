/**
 * Creates a skybox mesh. It can directly be added to the THREE.scene.
 * @param distanceFromOrigin the width, height and length of the skybox
 * @returns {Raycaster.params.Mesh|{}|*} A mesh object of the skybox.
 */
function addSkybox(distanceFromOrigin, scene) {
    // Locate all skybox texures
    var resLoc = "./ModelsAndTextures/";
    var posx = resLoc + "posx.jpg";
    var negx = resLoc + "negx.jpg";
    var posy = resLoc + "posy.jpg";
    var negy = resLoc + "negy.jpg";
    var posz = resLoc + "posz.jpg";
    var negz = resLoc + "negz.jpg";

    var directions = [posx, negx, posy, negy, posz, negz];

    // Makes six materials based on the six skybox textures
    var materialArray = [];
    var loader = new THREE.TextureLoader();

    for (var i = 0; i < 6; i++) {
        materialArray.push(new THREE.MeshBasicMaterial({
            map: loader.load(directions[i]),
            side: THREE.BackSide
        }));
    }

    // Make a box geometry to which the previous materials can be applied to
    var skyGeometry = new THREE.CubeGeometry(distanceFromOrigin, distanceFromOrigin, distanceFromOrigin);

    // Return the mesh of the skybox
    scene.add(new THREE.Mesh(skyGeometry, materialArray));
}