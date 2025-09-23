const form =document.querySelector("form");
// first try 
// let nameflag=false;
// let emailflag=false;
// let passflag=false;
// let confflag=false;
// let imgflag=false;
// let phoneflag=true;

function error_msg(id,msg,color="red"){
    document.getElementById(id).innerText=msg;
    document.getElementById(id).style.color=color;
    document.getElementById(id).style.marginLeft="155px";

}
function checkname(){
    if(form["name"].value.trim()===""||!/^[a-zA-Z0-9_]{3,20}$/.test(form["name"].value.trim())){
     error_msg("nameerror","invalid name");
     return false;
    }
    else {
        error_msg("nameerror", "");
        return true;
    }
   
}
 form["name"].onblur=checkname;
function checkemail(){
    if(form["email"].value.trim()===""||!/^[a-zA-Z0-9_]{3,25}@[a-z]+\.[a-z]{2,}$/.test(form["email"].value.trim())){
     error_msg("emailerror","invalid email");
     return false;
    }
    else {
        error_msg("emailerror", "");
        return true;
    }
}
form["email"].onblur=checkemail;
function checkphone(){
    if(!/^01[0-9]{9}$/.test(form["phone"].value.trim())&&form["phone"].value.trim()!==""){
     error_msg("phoneerror","invalid phone number");
     return false;
    }
    else {
        error_msg("phoneerror", "");
        return true;
    }
}
form["phone"].onblur=checkphone;
function checkpass(){
   if(form["password"].value.trim()===""||form["password"].value.trim().length<8){
     error_msg("passerror","password is required and must be 8 characters or more");
     return false;
    }
    else {
        error_msg("passerror", "");
        return true;
    }
}
form["password"].onblur=checkpass;

function checkconf(){
   if(form["confpass"].value.trim()===""||form["password"].value.trim()!==form["confpass"].value.trim()){
     error_msg("conferror","passwords doesn't match");
     return false;
    }
    else {
        error_msg("conferror", "");
        return true;
    }
}
form["confpass"].onblur=checkconf;

function checkimg (){
   if( form["img"].files.length === 0|| (form["img"].files[0].name.split(".").pop().toLowerCase() !== "jpg"&&
   form["img"].files[0].name.split(".").pop().toLowerCase() !== "png" &&form["img"].files[0].name.split(".").pop().toLowerCase() !== "jpeg")){
     error_msg("imgerror","invalid input (please upload a jpg or png or jpeg file)");
     return false;
    }
    else {
        error_msg("imgerror", "");
        return true;
    }
}
form["img"].onblur=checkimg;

form.onsubmit=function(){
    return (checkname()&&checkemail()&&checkphone()&&checkpass()&&checkconf()&&checkimg());
}

