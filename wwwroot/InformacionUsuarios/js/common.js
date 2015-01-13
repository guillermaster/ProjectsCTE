function popup(url, width, height, scrollbars, id) {
	window.open(url, id, 'toolbar=0,scrollbars='+scrollbars+',location=0,statusbar=0,menubar=0,resizable=0,width='+width+',height='+height);
}

function clear_textbox(obj){
	obj.value='';
}

function clearLabel(id)
        {
            document.getElementById('lblMensaje').innerHTML = '';
        }
        
function zoomText(Accion,Elemento){
//inicializacion de variables y parámetros
try{
var obj=document.getElementById(Elemento);
var max = 170 //tamaño máximo del fontSize
var min = 127 //tamaño mínimo del fontSize
if (obj.style.fontSize==""){
obj.style.fontSize="107%";
}
var actual=parseInt(obj.style.fontSize); //valor actual del tamaño del texto
var incremento=20;// el valor del incremento o decremento en el tamaño

//accion sobre el texto
if( Accion=="reestablecer" ){
obj.style.fontSize="107%"
}
if( Accion=="aumentar" && ((actual+incremento) <= max )){
valor=actual+incremento;
obj.style.fontSize=valor+"%"
}

if( Accion=="disminuir" && ((actual+incremento) >= min )){
valor=actual-incremento;
obj.style.fontSize=valor+"%"
}
}
catch(err){}
}


if (document.layers) window.captureEvents(Event.MOUSEDOWN);
document.oncontextmenu=new Function("return false")
document.onselectstart=new Function ("return false")