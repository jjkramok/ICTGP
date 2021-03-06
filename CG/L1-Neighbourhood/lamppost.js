/**
 * Adds a new lamppost to the scene
 * @param x
 * @param y
 * @param z
 * @param h the total height of the lamppost
 * @param r the radius or thickness of the lamppost
 * @param scene the scene to which the lamppost will be added
 */
function addLamppost(x, y, z, h, r, scene) {
    var loader = THREE.TextureLoader();

    // Create the post for the lamp
    var postRadius = r / 8;
    var postHeight = h * 11 / 12; // 5 / 6 fraction of height as post height
    var amountOfFaces = 12;
    var postGeo = new THREE.CylinderGeometry(postRadius, postRadius, postHeight, amountOfFaces);
    var postTexture = new THREE.TextureLoader().load("./ModelsAndTextures/lamppost_texture_00.jpg");

    var postMat = new THREE.MeshStandardMaterial({map: postTexture});
    var post = new THREE.Mesh(postGeo, postMat);
    post.position.set(x, y + h / 2, z); // Cylinders are drawn both down and upwards, so translate up.

    // Create the lamp itself
    var lampRadius = r;
    var lampBottomRadius = r / 2;
    var lampHeight = h / 12; // one sixth of the height
    var lampGeo = new THREE.CylinderGeometry(lampRadius, lampBottomRadius, lampHeight, amountOfFaces);
    var lampMat = new THREE.MeshStandardMaterial({
        metalness: 0.1,
        roughness: 0.9,
        color: 0xf4ed24
    });
    var lamp = new THREE.Mesh(lampGeo, lampMat);

    // Match the lamp's position to the height of the post.
    lamp.position.set(x, y + postHeight + lampHeight / 2, z);

    // Add both meshes to the scene
    scene.add(post);
    scene.add(lamp);
}