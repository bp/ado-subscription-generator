
import os
import logging
import json
from tokenize import String
import requests
from typing import Any, Dict
import azure.functions as func
from datetime import timedelta, datetime
from concurrent.futures import ThreadPoolExecutor, as_completed
from azure.storage.blob import BlockBlobService
from azure.devops.connection import Connection
from msrest.authentication import BasicAuthentication
from azure.devops.v6_0.build import models

def getindividualLogFiles(url :String):

    try:
            org_name = url.split('/')[3]
            projectId = url.split('/')[4]
            buildId = url.split('/')[8]
            container_name = 'landing'
            blobPath = f"/controls-build-timelines/{datetime.now():%Y-%m-%d}/{org_name}/{projectId}/{buildId}/{buildId}.json"

            block_blob_service = BlockBlobService(account_name=os.environ["SBACCOUNTNAME"], account_key=os.environ["SBACCOUNTKEY"]) 
            endpoint = f"https://dev.azure.com/{org_name}"

            credentials = BasicAuthentication('', os.environ["access_token"])
            connection = Connection(base_url=endpoint, creds=credentials)
            buildTimeLineJson = connection.clients.get_build_client().get_build_timeline(project=projectId, build_id=buildId)

            block_blob_service.create_blob_from_text(container_name, blobPath, json.dumps(buildTimeLineJson, indent=4,  sort_keys=True, default=str), encoding='utf-8')


    except Exception as ex:
            logging.error(f"Exception occurred while downloading the build timeline : {ex}")
            requests.post(os.environ["SLACK_END_POINT"] , data = { "text": f":no_entry: *ADF Durable function error* :runner: 'Build logs durable function exception' :clock7: '{datetime.now()}' :office: '{org_name}' :collision: '400' {str(ex)}"})
            raise Exception(f'{ex} ')

    return ""
