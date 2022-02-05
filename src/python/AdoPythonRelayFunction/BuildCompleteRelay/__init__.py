import logging
import json

import azure.functions as func
from .downloadBuildLogs import getindividualLogFiles


def main(msg: func.ServiceBusMessage):

    serviceBusMessageValue = msg.get_body().decode('utf-8')
    jsonsValOfEvent = json.loads(serviceBusMessageValue)

    buildLogsUrl = jsonsValOfEvent["resource"]["_links"]["timeline"]["href"]
    getindividualLogFiles(buildLogsUrl)
    logging.info('Python ServiceBus queue trigger processed message: %s',
                 msg.get_body().decode('utf-8'))

    return serviceBusMessageValue
