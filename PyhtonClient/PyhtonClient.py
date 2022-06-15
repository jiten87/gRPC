# Copyright 2015 gRPC authors.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
"""The Python implementation of the GRPC helloworld.Greeter client."""

from __future__ import print_function

import logging
import json
from pathlib import Path
from unicodedata import name
from google.protobuf.json_format import Parse, ParseDict

import grpc

import greet_pb2
import greet_pb2_grpc


def run():
    # NOTE(gRPC Python Team): .close() is possible on a channel and should be
    # used in circumstances in which the with statement does not fit the needs
    # of the code.
    with grpc.insecure_channel('localhost:5005') as channel:
        response = ""
        stub = greet_pb2_grpc.GreeterStub(channel)
        ##greeting = greet_pb2.Greeting(first_name ="jiten",last_name ="M")
        ##request = greet_pb2.GreetRequest(greeting = greeting)
        ##response = stub.Greet(request)

        # JSON file
        data = ""
        str = ""
        p = Path(__file__).with_name('keygen.json')
        with p.open('rb') as f:
            # Reading from file
            str =f.read()
            data = json.loads(str)

        
        reqcreatekey = ParseDict(data, greet_pb2.RequestCreateKey(), ignore_unknown_fields=True)
        response = stub.CreateKey(reqcreatekey)

    print(response.response)


if __name__ == '__main__':
    logging.basicConfig()
    run()

