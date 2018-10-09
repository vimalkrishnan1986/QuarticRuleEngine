Conceptual Approach

I would prefer microservice based appproach and that is how this has been implemented now.

The project will have

Rest Layer


1) Manging Rules (This controller will have method to post the rule and the  same will be saved in persitant storage)

2) Data (This will have actual data along with valid rule Id  which will be applied on top of data


Business Layer

Rule Engine Service which will fetch the rule associated with rule id and will  apply on the packet

This has been implemented using Lamda Expressions


Data

For now, we are just writing to simple text file 



If you had more time, what improvements would you make, and in what order of priority?


1) We would have developed end to end rule engine using following technologies 

 
1) Message Queue -This is where users can put the data packet .This will rule out the case of data loss
2) Message Bus Cloud-This is where users can expect the output 
3) Windows Workflow Foundation-Instead of writing code, we would have used windows workflow foundation 
4) Windows Service-This will have scheduuler which wil keep pooling into the message queue and process to message bus


