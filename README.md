# RandomAddress Generator

This is a WebAPI solution which generates random address. This is a GET api which can send back response in json, xml, and in plain text.

The endpoint is /randomizer/address

As per the requirement listed these fields are optional - state, stateCode, country. So these fields are not always send back in response. The decision whether to send these fields is currently made programatically.

The api can send back the response in plain text instead of xml or json if ?stringFormat=true is passed in the query string. From the example in requirement it seemed the address needs to be in plain text but in expected output it was mentioned the address to be in xml, json etc. To solve the ambiguity the api is designed to handle both cases.

The logic is written in AddressController.cs under Controllers.

By default the address in response is sent in application/json -

Execute from postman : GET - http://localhost:5795/randomizer/address
Response 1 -

{
    "house": "14",
    "street": "Mara Trafficway",
    "postalCode": "77860",
    "city": "Wisokyborough",
    "county": "Tayside",
    "state": "Montana",
    "stateCode": "KS",
    "country": "Mexico",
    "countryCode": "MEX"
}

For getting address in text/xml response, pass the header Accept with text/xml -
Response 2 -

Execute from postman : GET - http://localhost:5795/randomizer/address

<Address xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/RandomAddressGenerator.Controllers">
    <city>Osbornechester</city>
    <country>Mexico</country>
    <countryCode>MEX</countryCode>
    <county>Herefordshire</county>
    <house>7463</house>
    <postalCode>67635</postalCode>
    <state>New Mexico</state>
    <stateCode>WI</stateCode>
    <street>Altenwerth Way</street>
</Address>

For getting plain text address, please send ?stringFormat=true in query string, the response will be as below -

Execute from postman : GET - http://localhost:5795/randomizer/address?stringFormat=true

Response 3 - "2735 Aurelie Summit, West Virginia, Cumbria, Massachusetts, WY, Mexico, 06112-5510, MEX"

Response 4 -  "181 Sydni Glens, Port Pattie, Tyne and Wear, 10275, CAN"

Test Cases -

1. Get address - api returns address in json as shown in response 1 above.

2. Get address with Accept header (text/xml) - api returns address in xml as shown in response 2 above.

3. Get address with query string ?stringFormat=true with no Accept header - api returns address in plain text as shown in response 3 and 4.

4. Get address with query string ?stringFormat=false or any other value with no Accept header - api returns address in json as shown in response 1 above.

5. Get address with query string ?stringFormat=true with Accept header text/xml - api returns address in plain text as shown below -

<string xmlns="http://schemas.microsoft.com/2003/10/Serialization/">1478 Keira Turnpike, Arielleview, Worcestershire, Kentucky, ID, Mexico, 05626, MEX</string>

6. Get address with query string ?stringFormat=true with Accept header application/json - api returns address in plain text as shown - 

"7757 Kayla Meadow, Andersonland, County Londonderry, Ohio, MI, Netherlands, 36314, NLD"

7. Get with wrong url (missing randomizer in url etc) - api returns 404 response

8. Trying POST, PUT, DELETE etc - api returns 405 response with below message
    {
    "Message": "The requested resource does not support http method 'PUT'."
    }


Known issue -

The api is currently not hiding the fields state, stateCode, country if they have null when the response is sent in xml or json but this is handled for plaintext.
