

function CreateHouse(x, y, z, scene) {
    var houseBase = CreateHouseBase();
    var houseRoof = CreateHouseRoof();
    var houseWindow = CreateHouseWindow();

    houseBase.position.x += x + 2;
    houseBase.position.y += y + 1.5;
    houseBase.position.z += z + 3;

    for (var i = 0; i < houseWindow.length; i++) {
        houseWindow[i].position.x += x;
        houseWindow[i].position.y += y;
        houseWindow[i].position.z += z;
        scene.add(houseWindow[i]);
    }
    for (var i = 0; i < houseRoof.length; i++) {
        houseRoof[i].position.x += x;
        houseRoof[i].position.y += y + 3;
        houseRoof[i].position.z += z;
        scene.add(houseRoof[i]);
    }

    
    scene.add(houseBase);
}

function CreateHouseBase() {
    var houseBaseGeometry = new THREE.BoxBufferGeometry(4, 3, 6);
    var brickTexture = new THREE.TextureLoader().load('./ModelsAndTextures/Yellobrk.bmp');
    var brickMaterial = new THREE.MeshBasicMaterial({ map: brickTexture });
    var normalMaterial = new THREE.MeshNormalMaterial();

    houseBaseMesh = new THREE.Mesh(houseBaseGeometry, brickMaterial);
    return houseBaseMesh;
}

function CreateHouseRoof() {
    var uvs = [];
    uvs.push(new THREE.Vector2(0.0, 0.0));
    uvs.push(new THREE.Vector2(1.0, 0.0));
    uvs.push(new THREE.Vector2(1.0, 1.0));
    uvs.push(new THREE.Vector2(0.0, 1.0));
    uvs.push(new THREE.Vector2(0.5, 0.7));

    var roofGeometry = [new THREE.Geometry(), new THREE.Geometry()];

    for (var i = 0; i < roofGeometry.length; i++) {
        roofGeometry[i].vertices.push(new THREE.Vector3(0, 0, 0));
        roofGeometry[i].vertices.push(new THREE.Vector3(4, 0, 0));
        roofGeometry[i].vertices.push(new THREE.Vector3(2, 2, 0));
        roofGeometry[i].vertices.push(new THREE.Vector3(0, 0, 6));
        roofGeometry[i].vertices.push(new THREE.Vector3(4, 0, 6));
        roofGeometry[i].vertices.push(new THREE.Vector3(2, 2, 6));
    }
    roofGeometry[0].faces.push(new THREE.Face3(2, 1, 0));
    roofGeometry[0].faces.push(new THREE.Face3(5, 3, 4));

    roofGeometry[1].faces.push(new THREE.Face3(0, 3, 5));
    roofGeometry[1].faces.push(new THREE.Face3(1, 2, 5));
    roofGeometry[1].faces.push(new THREE.Face3(5, 2, 0));
    roofGeometry[1].faces.push(new THREE.Face3(5, 4, 1));

    roofGeometry[0].faceVertexUvs[0].push([uvs[4], uvs[1], uvs[0]]);
    roofGeometry[0].faceVertexUvs[0].push([uvs[4], uvs[1], uvs[0]]);

    roofGeometry[1].faceVertexUvs[0].push([uvs[2], uvs[3], uvs[0]]);
    roofGeometry[1].faceVertexUvs[0].push([uvs[0], uvs[3], uvs[2]]);
    roofGeometry[1].faceVertexUvs[0].push([uvs[0], uvs[1], uvs[2]]);
    roofGeometry[1].faceVertexUvs[0].push([uvs[2], uvs[1], uvs[0]]);

    roofGeometry.uvsNeedUpdate = true;

    var brickTexture = new THREE.TextureLoader().load('./ModelsAndTextures/Yellobrk.bmp');
    var brickMaterial = new THREE.MeshBasicMaterial({ map: brickTexture });

    var roofTexture = new THREE.TextureLoader().load('./ModelsAndTextures/roof.jpg');
    var roofMaterial = new THREE.MeshBasicMaterial({ map: roofTexture });

    houseRoofMesh = [
        new THREE.Mesh(roofGeometry[0], brickMaterial),
        new THREE.Mesh(roofGeometry[1], roofMaterial)
    ];
    
    return houseRoofMesh;
}

function CreateHouseWindow() {
    var material1 = new THREE.MeshLambertMaterial({ color: 0xee2211 });
    var material2 = new THREE.MeshPhongMaterial({ color: 0x1111ff });

    var geometries = [
        new THREE.Mesh(new THREE.TorusGeometry(0.6, 0.2, 20, 20), material1),
        new THREE.Mesh(new THREE.CylinderGeometry(0.6, 0.6, 0.1, 20, 20, false), material2),
    ];
    geometries[0].position.set(2, 3.5, 0);
    geometries[1].position.set(2, 3.5, 0);
    geometries[1].rotation.x = Math.PI / 2;

    return geometries;
}