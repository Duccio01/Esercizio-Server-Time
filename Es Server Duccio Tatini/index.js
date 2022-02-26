
const express = require("express");
//const bodyParser=require("body-parser");

const app=express();

//app.use(bodyParser.urlencoded());
//app.use(bodyParser.json);

const weekday = ["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];

const d = new Date();
//const day=d.getDay
let day =weekday[d.getUTCDay()];



let ballsNumber={
    entries:[
     {
         totalBalls:100,
         isMidnight:false
     }
    ]
}
let playerScore={
    entries:[
        {
            Name:"NomeGiocatore",
            survivalTime:0,
            ballsTaken:0
        }
    ]
}



app.get("/date", (req,res)=>{
    res.send(JSON.stringify(day));
});

app.get("/prova", (req,res)=>{
    res.send("Sei nella pagina di prova.");
});

app.get("/score", (req,res)=>{
    res.send(JSON.stringify(playerScore));
});
app.get("/score/:player", (req,res)=>{
    let playerName =req.params.player;
    let playerInfo=playerScore.entries.find(e=>e.name==playerName);

    res.send(JSON.stringify(playerInfo));
});

app.post("/set-score", (req,res)=>{
    console.log("Qualcuno ha mandato un nuovo punteggio");
    res.send("Risposta Ricevuta");
});
app.listen(3000,()=>console.log("Server in Funzione"));