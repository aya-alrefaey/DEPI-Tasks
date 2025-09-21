let fname="";
let fimg="";
let imgWidth=20;
let fontsize=16;
const validname=function(){
    let name=document.getElementById("name");
    let error=document.getElementById("nameerror");
    if(name.value.trim()===""){
error.innerText="Invalid Name";
error.style.color="red";
error.style.display="inline"
    }
    else{
      error.innerText="";  
      fname=name.value;
    }
}
const validimg=function(){
 let img=document.getElementById("img");
let error=document.getElementById("imgerror");
let ext=img.value.split(".").pop().toLowerCase();
if((ext!="jpg"&&ext!="png"&&ext!="jpeg")||img.value.trim()===""){
    error.innerText="Invalid Image";
    error.style.color="red";
    error.style.display="inline"
}
else{
      error.innerText="";  
      fimg=img.value;
    }
}

const display=function(){
    let dis=document.getElementById("display");
 
    if(fname!=""&&fimg!=""){
    dis.style.display="block";
    dis.style.marginTop="-10px";
    dis.style.border="1px solid black";
    dis.style.width="40%";
    // dis.style.height="200px";
    dis.style.textAlign="center";
    dis.style.padding="10px";
    dis.style.margin="0 auto";
    dis.style.borderRadius = "15px";
    const wel=document.getElementById("welcome");
    wel.innerText="Welcome "+fname;
    wel.style.marginTop="-5px"
    wel.style.marginBottom="5px"
   wel.style.fontSize = fontsize + "px";
    const btnn=document.getElementById("imgsize");
    btnn.innerText="image +";
    document.getElementById("userimg").src = fimg;
    document.getElementById("userimg").style.width = imgWidth + "%";
    let btn=document.getElementById("imgsize");
    btn.classList.add("btn");
    }

}
const imgbtn=document.getElementById("imgsize");
imgbtn.onclick=function(){
    if(imgWidth<70){
      imgWidth += 5; 
      document.getElementById("userimg").style.width = imgWidth + "%";
    }
    else{
document.getElementById("diserror").innerText="You Reach Maximum Width";
document.getElementById("diserror").style.color="red";

    }
   
}

const fonts=document.getElementById("sel");
fonts.onchange=function(){
 let msg=document.getElementById("welcome") ;
  fontsize = parseInt(fonts.value); 
 msg.style.fontSize = fontsize + "px";
}
const inc=document.getElementById("fontplus");
inc.onclick=function(){
    if(fontsize<40){
        fontsize+=2;
         let msg=document.getElementById("welcome")   ;
          msg.style.fontSize = fontsize + "px";
    }
}
const dec=document.getElementById("fontmin");
dec.onclick=function(){
    if(fontsize>10){
        fontsize-=2;
         let msg=document.getElementById("welcome")   ;
          msg.style.fontSize = fontsize + "px";
    }
}
document.getElementById("clickbtn").onclick=function(){
    validname();
    validimg();
    display();
}