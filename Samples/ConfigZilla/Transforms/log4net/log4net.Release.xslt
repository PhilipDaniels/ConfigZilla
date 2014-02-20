<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes"/>

  <!-- Put the log file into a particular folder for Release mode. This could be done with a .targets file,
       but is done this way to demonstrate how *.$(Configuration).xslt inclusion works. -->
  <xsl:template match="/log4net/appender/file">
    <xsl:copy>
      <xsl:apply-templates select="@*"/>
      <xsl:attribute name="value">
        <xsl:value-of select="concat('C:\temp\', @value)"/>
      </xsl:attribute>
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>