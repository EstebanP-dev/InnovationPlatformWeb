window.uploadToFirebase = (file) => {
    return new Promise((resolve, reject) => {
        const storageRef = firebase.storage().ref();
        const fileBlob = new Blob([file.content], { type: file.contentType });
        const uploadTask = storageRef.child('deliverables/' + file.identifier + '/' + file.name).put(fileBlob);

        uploadTask.on('state_changed', (snapshot) => {
            // Puedes manejar el progreso aquí si es necesario
        }, (error) => {
            // Maneja errores aquí
            reject(error);
        }, () => {
            // Subida completada con éxito
            uploadTask.snapshot.ref.getDownloadURL().then((downloadURL) => {
                resolve(downloadURL);
            });
        });
    });
};
