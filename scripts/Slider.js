let arrowRight = document.querySelector(".arrow-right");
let arrowLeft = document.querySelector(".arrow-left");
let listNews = document.querySelectorAll(".news");
let activeNews = [0,1];
arrowRight.onclick = swapRight;
arrowLeft.onclick = swapLeft;
function SetActiveNews(){
    for(let i = 0 ; i < listNews.length; i++){
        if(activeNews.indexOf(i)!=-1){
            listNews[i].style.display = ""
        }
        else{
            listNews[i].style.display = "none"
        }
    }
}
function swapRight(){
    AciveNewsPlus();
    SetActiveNews();
}
function swapLeft(){
    AciveNewsMinus();
    SetActiveNews();
}
function AciveNewsPlus(){
    
    if(activeNews[activeNews.length-1]<listNews.length-1){
        for(let i = 0 ; i<activeNews.length ; i++)
            activeNews[i]++;
        
    }
}
function AciveNewsMinus(){
    
    if(activeNews[0]>0){
        for(let i = 0 ; i<activeNews.length ; i++)
            activeNews[i]--;
    }
}

