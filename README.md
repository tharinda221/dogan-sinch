# Simple Binary Message Encoding Scheme

This project implements a simple binary message encoding scheme designed for passing messages between peers in a real-time communication application.

## Building the Project

To build the project, follow these steps:

1. Build the solution:
```console
dotnet build MessageEncoder.sln
```


## Running the Test Suite

To run the test suite using NUnit, execute the following command:
```console
dotnet build MessageEncoderTester.sln
nunit3-console.exe tests\bin\Debug\net7.0\SimpleMessageCodecTests.dll
```


## Folder Structure

- **Library**: Contains classes for exception handling, message handling, and the message codec.
- **MessageEncoder**: Demonstrates how to encode and decode messages using the library classes.
- **Program**: Provides an example execution of the encoding and decoding process.
- **Tests**: Contains test utilities and test cases for validating the encoding scheme.

## Encode Mechanism

### Encoding Headers (Header Names and Values)

1. **Header Count Encoding (1 byte)**:
   - The first byte of the encoded message represents the header count, indicating the number of headers in the message.
   - This count is limited to 63 headers, using the lower 6 bits of the byte to store the count.

2. **Header Name and Value Encoding (Variable Length)**:
   - Each header consists of a name-value pair, where both the name and the value are encoded as ASCII strings.
   - ASCII encoding is used for its simplicity and efficiency.
   - The encoding process for each header:
     - First, the name is encoded:
       - A 2-byte header name size is written to indicate the size of the name string.
       - The name string, an ASCII-encoded string, follows.
     - Next, the value is encoded in the same manner as the name.

3. **Size Limit for Header Names and Values (1023 bytes each)**:
   - Header name and value strings are subject to a size limit of 1023 bytes independently.

### Encoding Payload

1. **Payload Size Encoding (4 bytes)**:
   - Before the actual payload data, a 4-byte integer encodes the size of the payload in bytes.
   - This size indicator allows the decoder to determine the payload's length.

2. **Payload Data (Variable Length)**:
   - Following the payload size indicator, the payload data is encoded as binary bytes.
   - The payload size, as indicated earlier, determines the number of bytes to read for the payload data.

This encoding scheme balances simplicity and efficiency while meeting the requirements of real-time communication applications.

