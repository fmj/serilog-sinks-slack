﻿namespace SlackClient

open FSharp.Data
open FSharp.Data.HttpRequestHeaders

type ChatPostMessageResponse = 
    JsonProvider<""" {"ok":true,"channel":"XXXXXXXXX","ts":"1459354769.000004","message":{"text":"This is a test message!","username":"bot","type":"message","subtype":"bot_message","ts":"1459354769.000004"}, "error": "not_authed" } """>

module SlackClient = 
    open System.Net

    
    let internal ChatPostMessageRequest (token:string, channelId:string, msg:string, username:string, iconUrl:string) = ("https://slack.com/api/chat.postMessage?token=" + token + "&channel=" + channelId + "&username=" + username + "&icon_url=" + iconUrl + "&text=" + msg + "&pretty=1")
        
    let public SendMessage (token:string, channelId:string, message:string, username:string, iconUrl:string) = ChatPostMessageResponse.Load(ChatPostMessageRequest (token, channelId, message, username, iconUrl))
        
    let public SendMessageViaWebhooks (webhookUri:string, message:string) = Http.RequestString(webhookUri, headers = [ ContentType HttpContentTypes.Json ], body = TextRequest message)

    let public SendMessageViaWebhooks2 (webhookUri:string, message:string, proxy:string) = Http.Request(webhookUri, 
                                                                                                   httpMethod = "POST", 
                                                                                                   headers = [ ContentType HttpContentTypes.Json ],
                                                                                                   body = TextRequest message,
                                                                                                   customizeHttpRequest = fun req ->
                                                                                                       req.Proxy <- WebProxy(proxy,true);req)
    //String(webhookUri, headers = [ ContentType HttpContentTypes.Json ], body = TextRequest message)