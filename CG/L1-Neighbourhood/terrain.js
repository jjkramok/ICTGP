/**
 * Builds and adds a terrain, or underground, for the given scene.
 * @param width
 * @param height
 * @param scene
 */
function addTerrain(width, height, scene) {
    // Fetch the texture for the terrain
    var terrainTexture = new THREE.TextureLoader().load("./ModelsAndTextures/brick_pavement.jpg");

    // Repeatedly wrap the texture, otherwise the texture will be scaled up to match the geometry.
    terrainTexture.wrapS = THREE.RepeatWrapping;
    terrainTexture.wrapT = THREE.RepeatWrapping;
    terrainTexture.repeat.set(128, 128);

    // Create a material from the wrapped texture
    var terrainMaterial = new THREE.MeshBasicMaterial({map: terrainTexture});

    // Make a flat geometry for the terrain
    var terrainGeometry = new THREE.PlaneBufferGeometry(width, height, 8, 8);

    // Build the terrain mesh
    var plane = new THREE.Mesh(terrainGeometry, terrainMaterial);

    // Rotate the terrain plane by pi/2 rad to align it with the X/Z plane, and not the X/Y plane.
    plane.rotateX(-Math.PI / 2);

    // Finally add the mesh to the scene
    scene.add(plane);
}