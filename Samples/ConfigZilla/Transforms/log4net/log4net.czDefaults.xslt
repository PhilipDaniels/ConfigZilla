<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes"/>

  <!-- Set the conversion patterns in all appenders -->
  <xsl:template match="//conversionPattern/@value">
    <xsl:attribute name="value">
      <xsl:value-of select="'%date [%thread] %-5level %logger - %message%newline'"/>
    </xsl:attribute>
  </xsl:template>

</xsl:stylesheet>