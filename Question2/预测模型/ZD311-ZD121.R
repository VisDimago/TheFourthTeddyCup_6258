d<-read.csv("D:\\大学\\网络挖掘比赛试题\\StationFlowInfo\\ZD311至ZD121.csv",",",header=T)
x<-ts(d[,2],start=c(2015,1,1),frequency=365)
plot(x)
arima1=auto.arima(x)
arima1
library(zoo)
library(forecast)
d<-read.csv("D:\\大学\\网络挖掘比赛试题\\StationFlowInfo\\ZD311至ZD121.csv",",",header=T)
x<-ts(d[,2],start=c(2015,1,1),frequency=365)
x.fit<-arima(x,order=c(0,0,1))
x.fore<-forecast(x.fit,h=12)
x.fore
win.graph()
plot(x.fore)

d<-read.csv("D:\\大学\\网络挖掘比赛试题\\2\\ZD311至ZD121.csv",",",header=T)
x<-ts(d[,2],start=1)
plot(x)
arima1=auto.arima(x)
arima1
library(zoo)
library(forecast)
d<-read.csv("D:\\大学\\网络挖掘比赛试题\\2\\ZD311至ZD121.csv",",",header=T)
x<-ts(d[,2],start=1)
x.fit<-arima(x,order=c(0,0,0))
x.fore<-forecast(x.fit,h=2)
x.fore
win.graph()
plot(x.fore)