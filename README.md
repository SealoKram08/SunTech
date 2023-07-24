# SunTech
Challenge:

Create a simple backend that processes customer information using the .Net Stack. Follow
the specific instructions below:

1. Use portal.azure.com for free.
2. Use a lightweight, consumption-based Azure Function App for the REST API. This API
should accept POST methods and save the data to the NoSQL database below. Use
PostMan – there's no need to create a frontend.
3. Use NoSQL CosmosDb to save customer info.
The customer info in NoSQL should have these fields:
{
"FirstName": "Mark",
"LastName": "Monurst",
"BirthdayInEpoch": 125566,
"Email": "mark@gmail.com"
}
4. You should save these data using a CQRS + event-driven approach.
5. Create another FunctionApp that reacts to any data changes, whether updates or
inserts.
6. Create an Azure Event Grid.
7. The FunctionApp you create in step 6 will publish the data as a message to the Azure
Event Grid.
8. You should see your message received in the Azure Event Grid.
9. Done.

Acceptance Criteria:
1. Should use Azure.
2. Should use a .Net C# Function App.
3. Should use CosmosDb NoSQL.
4. I should see an event append-only write container and a materialized view
container.
5. I should see that data is received as event messages in the Azure Event Grid.
