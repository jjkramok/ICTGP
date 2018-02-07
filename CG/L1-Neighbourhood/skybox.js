/**
 * Creates a skybox mesh. It can directly be added to the THREE.scene.
 * @param distanceFromOrigin the width, height and length of the skybox
 * @returns {Raycaster.params.Mesh|{}|*} A mesh object of the skybox.
 */
function createSkybox(distanceFromOrigin) {
    // Locate all skybox texures
    var resLoc = "./ModelsAndTextures/";
    var posx = resLoc + "posx.jpg";
    var negx = resLoc + "negx.jpg";
    var posy = resLoc + "posy.jpg";
    var negy = resLoc + "negy.jpg";
    var posz = resLoc + "posz.jpg";
    var negz = resLoc + "negz.jpg";

    var directions = [posx, negx, posy, negy, posz, negz];

    /*var cubeTextures = THREE.ImageUtils.loadTextureCube(directions);

    // Initialize a shader for the skybox cube
    var shader = THREE.ShaderUtils.lib["cube"];
    var uniforms = THREE.UniformsUtils.clone (shader.uniforms);
    uniforms['tCube'].texture = cubeTextures;
    var skyMaterial = new THREE.MeshShaderMaterial({
        fragmentShader  : shader.fragmentShader,
        vertexShader    : shader.vertexShader,
        uniforms    : uniforms
    });

    skyboxMesh = new THREE.Mesh( new THREE.CubeGeometry(distanceFromOrigin, distanceFromOrigin, distanceFromOrigin, 1, 1, 1, null, true), skyMaterial);
    skyboxMesh.doubleSided = true;
    return skyboxMesh;*/

    // original way to prepare textures for the skybox
    var materialArray = [];
    var loader = new THREE.TextureLoader();

    for (var i = 0; i < 6; i++) {
        materialArray.push(new THREE.MeshBasicMaterial({
            map: loader.load(directions[i]),
            side: THREE.BackSide
        }));
    }

    var skyGeometry = new THREE.CubeGeometry(distanceFromOrigin, distanceFromOrigin, distanceFromOrigin);

    return new THREE.Mesh(skyGeometry, materialArray);
}