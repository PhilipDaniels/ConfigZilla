<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <!-- Set the conversion patterns in all appenders. Some of these settings are "expensive" according to the log4net documentation. -->
  <xsl:template match="//conversionPattern/@value">
    <xsl:attribute name="value">
      <xsl:value-of select="'%date [%thread] %-5level %20.20method - %message - %logger%newline'"/>
    </xsl:attribute>
  </xsl:template>

</xsl:stylesheet>