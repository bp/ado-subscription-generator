import logging

import azure.functions as func


def main(msg: func.ServiceBusMessage):

    serviceBusMessageValue = msg.get_body().decode('utf-8')
    logging.info('Python ServiceBus queue trigger processed message: %s',
                 serviceBusMessageValue)

    return serviceBusMessageValue
