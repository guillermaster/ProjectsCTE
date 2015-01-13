function ShowNewFolderControls() {
    //alert("test");
    document.getElementById('toolbar').style.height = "150px";
    /*var divNewFolder = document.getElementById('divNewFolder');
    divNewFolder.style.visibility = 'visible';*/
}

function showWaitNewDocUpload() {
    if (document.getElementById('FileUpload1').value.length > 0) {
        document.getElementById('upprogNewDoc').style.display = 'block';
    }
}

function showWaitSharing() {
    document.getElementById('upprogSharing').style.display = 'block';
}