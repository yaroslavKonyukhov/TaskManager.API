# Konyukhov Yaroslav it3-2303. Midterm task for ASP.NET subject - TaskManager.API 

## Answer to block 3
If we were to implement a NotificationService to send emails triggered by the OnTaskCompleted event, 
I would choose an Asynchronous Integration Pattern using a Message Broker (e.g., RabbitMQ or Azure Service Bus).

Why Asynchronous?
- Decoupling: The Task Service should not care if the email was actually sent or if the notification system is currently online. Its only job is to complete the task.
- Performance: Sending an email via an external SMTP server can be slow. In a Synchronous (HTTP/REST) pattern, the user would have to wait for the email to be sent 
before getting a "Task Completed" response. With an asynchronous approach, the task is updated instantly, and the notification happens in the background.
- Resilience: If the NotificationService is down, the message stays in the queue (RabbitMQ). Once the service is back online, it processes the message 
and sends the email without losing data.

Technologies to use:
- RabbitMQ / Kafka: To act as the Message Broker between the Task Service (Producer) and the Notification Service (Consumer).
- MassTransit: A library for .NET that simplifies working with message buses and handles retries and circuit breakers automatically.
- MailKit / SendGrid: For the actual email delivery logic within the NotificationService.

Conclusion:
While a simple HTTP/REST (Synchronous) call is easier to implement initially, it creates a "tight coupling" and reduces system reliability. 
For a scalable, production-ready system, an Asynchronous pattern is the superior choice.