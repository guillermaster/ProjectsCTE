function popup(url, width, height, scrollbars, id) {
    window.open(url, id, 'toolbar=0,scrollbars=' + scrollbars + ',location=0,statusbar=0,directories=0,menubar=0,resizable=0,width=' + width + ',height=' + height);
}

function popupAndWriteHtml(width, height, scrollbars, id, html) {
    popWin = window.open('', id, 'toolbar=0,scrollbars=' + scrollbars + ',location=0,statusbar=0,directories=0,location=0,menubar=0,resizable=0,width=' + width + ',height=' + height);
    popWin.moveTo(280, 200);
    popWin.document.write("<html><head><title>CTG - Código Electrónico de Pago</title>");
    popWin.document.write("<link href='../CSS/StyleSheetPrintView.css' rel='stylesheet' type='text/css' /></head>");
    popWin.document.write("<body>");
    popWin.document.write(html);
    popWin.document.write("<div align='center'><a href='#' onclick='javascript:window.print();window.close();'>Imprimir</a></div>");
    popWin.document.write("</body></html>");
    popWin.document.title = "CTG - Código Electrónico de Pago";
    popWin.document.close();
    //popWin.print();
}